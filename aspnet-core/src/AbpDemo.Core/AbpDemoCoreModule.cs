using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo
{
    public class AbpDemoCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoCoreModule).GetAssembly());
        }
    }
}