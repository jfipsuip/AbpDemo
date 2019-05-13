using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoCoreModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoApplicationModule).GetAssembly());
        }
    }
}