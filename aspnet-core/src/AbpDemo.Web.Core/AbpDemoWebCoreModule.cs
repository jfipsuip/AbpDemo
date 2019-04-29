using System;
using System.Text;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpDemo;
using AbpDemo.Authentication.JwtBearer;
using AbpDemo.Configuration;
using AbpDemo.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;



#if FEATURE_SIGNALR
using Abp.Web.SignalR;
#elif FEATURE_SIGNALR_ASPNETCORE
using Abp.AspNetCore.SignalR;
#endif

namespace AbpDemo
{
    [DependsOn(
         typeof(AbpDemoApplicationModule),
         typeof(AbpDemoEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)//,
         //typeof(AbpRedisCacheModule)
#if FEATURE_SIGNALR 
        ,typeof(AbpWebSignalRModule)
#elif FEATURE_SIGNALR_ASPNETCORE
        ,typeof(AbpAspNetCoreSignalRModule)
#endif
     )]
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
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AbpDemoConsts.ConnectionStringName
            );

            // Use database for language management
            //Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(AbpDemoApplicationModule).GetAssembly()
                 );

            //设置不隐藏异常信息
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = Convert.ToBoolean(_appConfiguration["AppSettings:SendAllExceptionsToClients"]);

            #region 缓存配置
            /*
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
            */
            #endregion

            ConfigureTokenAuth();
            ConfigureAppSettings();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            //tokenAuthConfig.Expiration = TimeSpan.FromMinutes(60);
            tokenAuthConfig.Expiration = TimeSpan.FromSeconds(30);
        }

        private void ConfigureAppSettings()
        {
            //IocManager.Register<AppSettingsCfg>();
            //var appSettingsConfig = IocManager.Resolve<AppSettingsCfg>();

            //appSettingsConfig.SystemCode = _appConfiguration["AppSettings:SystemCode"];
            //appSettingsConfig.FtpIP = _appConfiguration["AppSettings:FtpIP"];
            //appSettingsConfig.FtpUid = _appConfiguration["AppSettings:FtpUid"];
            //appSettingsConfig.FtpPwd = _appConfiguration["AppSettings:FtpPwd"];
            //appSettingsConfig.FtpVUid = _appConfiguration["AppSettings:FtpVUid"];
            //appSettingsConfig.FtpVPwd = _appConfiguration["AppSettings:FtpVPwd"];
            //appSettingsConfig.FtpDir = _appConfiguration["AppSettings:FtpDir"];
            //appSettingsConfig.FtpDirWorkFlow = _appConfiguration["AppSettings:FtpDirWorkFlow"];

            //appSettingsConfig.ApiSmartCloudInfra = _appConfiguration["AppSettings:ApiSmartCloudInfra"];
            //appSettingsConfig.ApiSmartCloudWorkFlow = _appConfiguration["AppSettings:ApiSmartCloudWorkFlow"];
            //appSettingsConfig.TestPhoneNumber = _appConfiguration["AppSettings:TestPhoneNumber"];


        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebCoreModule).GetAssembly());
        }
    }
}
