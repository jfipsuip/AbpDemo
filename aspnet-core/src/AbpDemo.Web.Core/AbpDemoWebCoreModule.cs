using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using AbpDemo.Authentication.JwtBearer;
using AbpDemo.Configuration;
using AbpDemo.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoEntityFrameworkModule),
        typeof(AbpAspNetCoreModule))]
    public class AbpDemoWebCoreModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AbpDemoWebCoreModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "server=windows2008;uid=sa;pwd=sa;database=AbpDemo;";

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                typeof(AbpDemoApplicationModule).GetAssembly());

            //设置不隐藏异常信息
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
            #region 缓存配置
            //配置所有Cache的默认过期时间30天
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromDays(30);
            });

            //配置指定的Cache过期时间为45秒
            Configuration.Caching.Configure("LoginTokenCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(45);
            });

            Configuration.Caching.UseRedis(options =>
            {
                options.ConnectionString = _appConfiguration["Abp:RedisCache:ConnectionString"];
                options.DatabaseId = _appConfiguration.GetValue<int>("Abp:RedisCache:DatabaseId");
            });
            #endregion

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebCoreModule).GetAssembly());
        }
    }
}
