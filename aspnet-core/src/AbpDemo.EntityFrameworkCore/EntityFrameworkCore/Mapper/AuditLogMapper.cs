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
            builder.ToTable("AuditLog");

            // Primary Key
            builder
                .HasKey(t => t.Id);

            // Property
            builder
                .Property(t => t.SystemCode)
                .HasMaxLength(100);

            builder
                .Property(t => t.TenantId);

            builder
                .Property(t => t.UserId);

            builder
                .Property(t => t.ServiceName)
                .HasMaxLength(AuditLog.MaxServiceNameLength);

            builder
                .Property(t => t.MethodName)
                .HasMaxLength(AuditLog.MaxMethodNameLength);

            builder
                .Property(t => t.Parameters)
                .HasMaxLength(AuditLog.MaxParametersLength);

            builder
                .Property(t => t.ExecutionTime);

            builder
                .Property(t => t.ExecutionDuration);

            builder
                .Property(t => t.ClientIpAddress)
                .HasMaxLength(AuditLog.MaxClientIpAddressLength);

            builder
                .Property(t => t.ClientName)
                .HasMaxLength(AuditLog.MaxClientNameLength);

            builder
                .Property(t => t.BrowserInfo)
                .HasMaxLength(AuditLog.MaxBrowserInfoLength);

            builder
                .Property(t => t.Exception)
                .HasMaxLength(AuditLog.MaxExceptionLength);

            builder
                .Property(t => t.ImpersonatorUserId);

            builder
                .Property(t => t.ImpersonatorTenantId);

            builder
                .Property(t => t.CustomData)
                .HasMaxLength(AuditLog.MaxCustomDataLength);
        }
    }
}