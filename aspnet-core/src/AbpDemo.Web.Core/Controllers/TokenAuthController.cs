using Abp.AspNetCore.Mvc.Controllers;
using Abp.UI;
using AbpDemo.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : AbpController
    {
        private readonly TokenAuthConfiguration _configuration;

        public TokenAuthController(TokenAuthConfiguration configuration)
        {
            _configuration = configuration;
        }

        //从缓存中获取token（只有45秒缓存）
        [AllowAnonymous]
        [HttpGet]
        //[EnableCors("localhost")]
        public async Task<AuthenticateResultModel> GetTokenFromCache()
        {
            AuthenticateResultModel output;
            try
            {
                //string serverState = _session.GetString("serverState");
                string serverState = HttpContext.Request.Query["serverState"].FirstOrDefault() ?? "";

                //var cachemodel = await _cacheManager.GetCache("LoginTokenCache").GetAsync(ticket, null);
                //output = cachemodel as AuthenticateResultModel;
                output = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticateResultModel>(serverState);
            }
            catch
            {
                throw new UserFriendlyException("登录异常", "所请求的访问token不存在");
            }
            return output;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> GetTicket([FromBody] LoginFromBodyModel model)
        {
            var user = new SysUserInfo { Id = 235, UserAccout = "testAccout", UserName = "testName", UserStatus = 1, };
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.Sid, user.UserAccout));
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

            //var loginResult = new LoginResult(
            //    user,
            //    identity
            //);
            //switch (loginResult.Result)
            //{
            //    case LoginResultType.Success:
            //        ;
            //        break;
            //    case LoginResultType.InvalidUserNameOrEmailAddress:
            //        throw new UserFriendlyException("登录失败：无效的登录用户名");
            //    case LoginResultType.InvalidPassword:
            //        throw new UserFriendlyException("登录失败：无效的登录密码");
            //    case LoginResultType.InvalidEmployee:
            //        throw new UserFriendlyException("登录失败：无效的关联员工");
            //    case LoginResultType.LockedOut:
            //        throw new UserFriendlyException($"登录失败：用户 {loginResult.User.UserAccout} 未激活，不能登录");
            //    default:
            //        throw new UserFriendlyException("登录失败：用户名或密码无效");
            //}
            var now = DateTime.UtcNow;
            TimeSpan? expiration = null;
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: identity.Claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            AuthenticateResultModel authTokenModel = new AuthenticateResultModel
            {
                AccessToken = accessToken,
            };
            string serverState = Newtonsoft.Json.JsonConvert.SerializeObject(authTokenModel);
            return serverState;
        }

    }
}
