using Abp.AutoMapper;
using Abp.Modules;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoCoreModule),
        typeof(AbpAutoMapperModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
    }
}
