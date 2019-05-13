using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace AbpDemo
{
    [DependsOn(
        typeof(AbpDemoCoreModule),
        typeof(AbpAutoMapperModule))]
    public class AbpDemoApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(AbpDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}