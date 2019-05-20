using AbpDemo.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.EntityFrameworkCore.Mapper
{
    public partial class AuditLogMapper : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("Sys_AuditLog");

            // Primary Key
            builder
                .HasKey(t => t.Id);
        }
    }
}
