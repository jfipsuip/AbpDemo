using AbpDemo.Areas;
using AbpDemo.Depts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.EntityFrameworkCore.Mapper
{
    public partial class DeptMapper : IEntityTypeConfiguration<Dept>
    {
        public void Configure(EntityTypeBuilder<Dept> builder)
        {
            builder.ToTable("Sys_Dept");

            // Primary Key
            builder
                .HasKey(t => t.Id);

            // Properties
            builder
                .Property(t => t.Name)
                .HasMaxLength(100);

            builder
                .Property(t => t.Code)
                .HasMaxLength(100);

            builder
                .Property(t => t.AreaId);

            //实体映射关系
            builder
                .HasOne(t => t.Area)
                .WithMany()
                .HasForeignKey(t => t.AreaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
