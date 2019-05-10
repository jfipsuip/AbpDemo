using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo.Web.Host.Startup
{
    [DependsOn(typeof(AbpDemoWebCoreModule))]
    public class AbpDemoWebHostModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoWebHostModule).GetAssembly());
        }
    }
}