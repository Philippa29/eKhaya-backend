using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace eKhaya.EntityFrameworkCore
{
    public static class eKhayaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<eKhayaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<eKhayaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
