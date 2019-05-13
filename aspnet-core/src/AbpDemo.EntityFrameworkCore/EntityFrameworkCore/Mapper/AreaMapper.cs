using AbpDemo.Areas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.EntityFrameworkCore.Mapper
{
    public partial class AreaMapper : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Sys_Area");

            // Primary Key
            builder
                .HasKey(t => t.Id);

            // Properties
            builder
                .Property(t => t.Name)
                .HasMaxLength(100);

            builder
                .Property(t => t.AreaCode)
                .HasMaxLength(100);
        }
    }
}
