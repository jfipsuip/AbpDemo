using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using AbpDemo.EntityFrameworkCore;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoApplicationModule),
        typeof(AbpDemoEntityFrameworkModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpRedisCacheModule))]
    public class AbpDemoWebCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "server=windows2008;uid=sa;pwd=sa;database=AbpDemo;";

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                typeof(AbpDemoApplicationModule).GetAssembly());

            Configuration.Caching.UseRedis(options =>
            {
                options.ConnectionString = "172.16.4.121";
                options.DatabaseId = -1;
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebCoreModule).GetAssembly());
        }
    }
}
