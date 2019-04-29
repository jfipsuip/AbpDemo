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
        public override void PreInitialize()
        {


            //base.PreInitialize();
            //IocManager.Register<ICacheManager, AbpRedisCacheManager>();
            //如果Redis在本机,并且使用的默认端口,下面的代码可以不要
            //Configuration.Modules. = "Abp.Redis.Cache";

            //配置使用Redis缓存
            //Configuration.Modules.AbpConfiguration.DefaultNameOrConnectionString = "Abp.Redis.Cache";
            //Configuration.Caching.UseRedis();





        }

        public override void Initialize()
        {
            //IocManager.Register<IDiscoveryClient, DiscoveryClient>();
            //IocManager.RegisterAssemblyByConvention(typeof(WebAppApplicationModule).GetAssembly());

            var thisAssembly = typeof(AbpDemoApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}