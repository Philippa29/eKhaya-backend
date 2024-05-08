using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eKhaya.EntityFrameworkCore;
using eKhaya.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace eKhaya.Web.Tests
{
    [DependsOn(
        typeof(eKhayaWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class eKhayaWebTestModule : AbpModule
    {
        public eKhayaWebTestModule(eKhayaEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eKhayaWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(eKhayaWebMvcModule).Assembly);
        }
    }
}