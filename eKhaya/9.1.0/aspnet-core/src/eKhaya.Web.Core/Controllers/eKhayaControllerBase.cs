using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace eKhaya.Controllers
{
    public abstract class eKhayaControllerBase: AbpController
    {
        protected eKhayaControllerBase()
        {
            LocalizationSourceName = eKhayaConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
