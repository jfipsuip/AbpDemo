using Abp.EntityFrameworkCore;
using AbpDemo.SysAreas;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<SysArea> SysArea { get; set; }
        
        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options)
            : base(options)
        {
        }
    }
}
