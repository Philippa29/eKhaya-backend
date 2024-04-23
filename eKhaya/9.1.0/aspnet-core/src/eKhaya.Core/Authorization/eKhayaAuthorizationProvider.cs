using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace eKhaya.Authorization
{
    public class eKhayaAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_ProeprtyManager, L("PropertyManager"));
            context.CreatePermission(PermissionNames.Pages_Agents, L("Agents"));
            context.CreatePermission(PermissionNames.Pages_Residents, L("Residents"));
            context.CreatePermission(PermissionNames.Pages_Applicants, L("Applicants"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, eKhayaConsts.LocalizationSourceName);
        }
    }
}
