using Abp.EntityFrameworkCore;
using AbpDemo.SysAreas;
using AbpDemo.SysFileInfos;
using Microsoft.EntityFrameworkCore;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<FileInfo> FileInfo { get; set; }
        
        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options)
            : base(options)
        {
        }
    }
}
