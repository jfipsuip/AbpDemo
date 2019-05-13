using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options)
            : base(options)
        {
        }
    }
}
