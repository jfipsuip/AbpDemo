using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo
{
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void PostInitialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoApplicationModule).GetAssembly());
        }
    }
}