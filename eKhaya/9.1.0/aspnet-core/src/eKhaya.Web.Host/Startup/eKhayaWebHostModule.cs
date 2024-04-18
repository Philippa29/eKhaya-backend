using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using eKhaya.Configuration;

namespace eKhaya.Web.Host.Startup
{
    [DependsOn(
       typeof(eKhayaWebCoreModule))]
    public class eKhayaWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public eKhayaWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(eKhayaWebHostModule).GetAssembly());
        }
    }
}
