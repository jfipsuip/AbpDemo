using AbpDemo.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.EntityFrameworkCore.Mapper
{
    class PersonMapper : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Sys_Person");

            // Primary Key
            builder
                .HasKey(t => t.Id);

            // Properties
            builder
                .Property(t => t.FirstName)
                .HasMaxLength(100);

            builder
                .Property(t => t.LastName)
                .HasMaxLength(100);

            builder
                .Property(t => t.Address)
                .HasMaxLength(200);

            builder
                .Property(t => t.Sex)
                .HasMaxLength(10);
        }
    }
}
