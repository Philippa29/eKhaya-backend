using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eKhaya.Authorization;

namespace eKhaya
{
    [DependsOn(
        typeof(eKhayaCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class eKhayaApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<eKhayaAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(eKhayaApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
