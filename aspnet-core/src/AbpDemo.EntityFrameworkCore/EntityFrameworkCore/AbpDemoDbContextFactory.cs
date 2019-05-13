using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpDemo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AbpDemoDbContextFactory : IDesignTimeDbContextFactory<AbpDemoDbContext>
    {
        public AbpDemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AbpDemoDbContext>();

            AbpDemoDbContextConfigurer.Configure(builder, "Server=localhost; Database=AbpDemoDb; Trusted_Connection=True;");

            return new AbpDemoDbContext(builder.Options);
        }
    }
}
