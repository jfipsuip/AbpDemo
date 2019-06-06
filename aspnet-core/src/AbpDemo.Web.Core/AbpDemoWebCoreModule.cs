using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
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
