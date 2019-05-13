using Abp.EntityFrameworkCore;
using AbpDemo.Areas;
using AbpDemo.Depts;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Area> Area { get; set; }
        public DbSet<Dept> Dept { get; set; }

        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options)
            : base(options)
        {
        }
    }
}
