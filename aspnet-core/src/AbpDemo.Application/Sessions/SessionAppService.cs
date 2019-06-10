using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Runtime.Caching;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Application.Sessions
{
    public class SessionAppService : ApplicationService
    {

        public ICacheManager cacheManager { get; set; }

        [DisableAuditing]
        //[Abp.Authorization.AbpAuthorize]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                //Application = new ApplicationInfoDto
                //{
                //    Version = AppVersionHelper.Version,
                //    ReleaseDate = AppVersionHelper.ReleaseDate,
                //    Features = new Dictionary<string, bool>
                //    {
                //        { "SignalR", SignalRFeature.IsAvailable },
                //        { "SignalR.AspNetCore", SignalRFeature.IsAspNetCore }
                //    }
                //}
            };

            if (AbpSession.UserId.HasValue)
            {
                //output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }

        [AllowAnonymous]
        public void SetCache(string ticket, string value)
        {
            TimeSpan expireTime = TimeSpan.FromSeconds(100);
            cacheManager.GetCache("LoginTokenCache").Set(ticket, value, expireTime);
        }
        [AllowAnonymous]
        [DisableAuditing]
        public async Task<string> GetCache(string ticket)
        {
            var cachemodel = await cacheManager.GetCache("LoginTokenCache").GetAsync(ticket, null);
            return cachemodel.ToString();
        }

    }
}
