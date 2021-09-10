using Abp.EntityFrameworkCore;
using AbpDemo.Areas;
using AbpDemo.Depts;
using AbpDemo.Persons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace AbpDemo.EntityFrameworkCore
{
    public class AbpDemoDbContext : AbpDbContext
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Area> Area { get; set; }
        public DbSet<Dept> Dept { get; set; }

        public DbSet<Person> Person { get; set; }


        public AbpDemoDbContext(DbContextOptions<AbpDemoDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mappingInterface = typeof(IEntityTypeConfiguration<>);
            // Types that do entity mapping
            var mappingTypes = GetType().GetTypeInfo().Assembly.GetTypes()
                                .Where(x => x.GetInterfaces()
                                .Any(y => y.GetTypeInfo().IsGenericType
                                && y.GetGenericTypeDefinition() == mappingInterface));

            // Get the generic Entity method of the ModelBuilder type
            var entityMethod = typeof(ModelBuilder).GetMethods()
                                .Single(x => x.Name == "Entity"
                                        && x.IsGenericMethod
                                        && x.ReturnType.Name == "EntityTypeBuilder`1");

            foreach (var mappingType in mappingTypes)
            {
                // Get the type of entity to be mapped
                var genericTypeArg = mappingType.GetInterfaces().Single().GenericTypeArguments.Single();

                // Get the method builder.Entity<TEntity>
                var genericEntityMethod = entityMethod.MakeGenericMethod(genericTypeArg);

                // Invoke builder.Entity<TEntity> to get a builder for the entity to be mapped
                var entityBuilder = genericEntityMethod.Invoke(modelBuilder, null);

                // Create the mapping type and do the mapping
                var mapper = Activator.CreateInstance(mappingType);
                mapper.GetType().GetMethod("Configure").Invoke(mapper, new[] { entityBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
