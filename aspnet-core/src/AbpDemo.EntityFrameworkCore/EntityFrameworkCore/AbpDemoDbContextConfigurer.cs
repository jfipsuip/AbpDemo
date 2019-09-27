using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public static class AbpDemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpDemoDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AbpDemoDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
