using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using AbpDemo.Configuration;

namespace AbpDemo
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class AbpDemoCoreModule : AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDemoCoreModule).GetAssembly());
        }
    }
}
