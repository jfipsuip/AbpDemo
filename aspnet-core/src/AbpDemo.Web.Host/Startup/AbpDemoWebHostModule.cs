using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using AbpDemo.Configuration;

namespace AbpDemo.Web.Host.Startup
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AbpDemoWebCoreModule))]
    public class AbpDemoWebHostModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        public AbpDemoWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        //public override void PreInitialize()
        //{
        //    Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AbpDemoConsts.ConnectionStringName);

        //    Configuration.Navigation.Providers.Add<AbpDemoNavigationProvider>();

        //    Configuration.Modules.AbpAspNetCore()
        //        .CreateControllersForAppServices(
        //            typeof(AbpDemoApplicationModule).GetAssembly()
        //        );
        //}

        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebHostModule).GetAssembly());
        }
    }
}