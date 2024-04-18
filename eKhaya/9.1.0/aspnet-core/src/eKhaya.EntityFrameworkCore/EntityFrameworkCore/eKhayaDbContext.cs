using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using eKhaya.Authorization.Roles;
using eKhaya.Authorization.Users;
using eKhaya.MultiTenancy;

namespace eKhaya.EntityFrameworkCore
{
    public class eKhayaDbContext : AbpZeroDbContext<Tenant, Role, User, eKhayaDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public eKhayaDbContext(DbContextOptions<eKhayaDbContext> options)
            : base(options)
        {
        }
    }
}
