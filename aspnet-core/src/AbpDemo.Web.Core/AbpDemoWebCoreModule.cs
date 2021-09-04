using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpDemo.EntityFrameworkCore;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoEntityFrameworkModule),
        typeof(AbpAspNetCoreModule))]
    public class AbpDemoWebCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Server=localhost; Database=AbpDemoDb; Trusted_Connection=True;";

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                typeof(AbpDemoApplicationModule).GetAssembly());

            //设置不隐藏异常信息
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = true;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebCoreModule).GetAssembly());
        }
    }
}
