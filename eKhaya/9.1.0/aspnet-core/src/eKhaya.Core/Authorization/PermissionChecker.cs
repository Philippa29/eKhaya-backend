using Abp.Authorization;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;

namespace eKhaya.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
