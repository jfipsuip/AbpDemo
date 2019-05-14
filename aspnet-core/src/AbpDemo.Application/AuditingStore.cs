using Abp.Auditing;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.AuditLogs
{
    /// <summary>
    /// Implements <see cref="IAuditingStore"/> to save auditing informations to database.
    /// </summary>
    public class AuditingStore222 : IAuditingStore, ITransientDependency
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;

        /// <summary>
        /// Creates  a new <see cref="AuditingStore222"/>.
        /// </summary>
        public AuditingStore222(IRepository<AuditLog, long> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }

        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            return _auditLogRepository.InsertAsync(AuditLog.CreateFromAuditInfo(auditInfo));
        }
    }
}