using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.AuditLogs
{
    /// Implements <see cref="IAuditingStore"/> to save auditing informations to database.
    /// </summary>
    public class AuditingStore : IAuditingStore, ITransientDependency
    {
        public IRepository<AuditLog, long> AuditLogR { get; set; }

        public virtual async Task SaveAsync(AuditInfo auditInfo)
        {
            await AuditLogR.InsertAsync(AuditLog.CreateFromAuditInfo(auditInfo));
        }
    }
}