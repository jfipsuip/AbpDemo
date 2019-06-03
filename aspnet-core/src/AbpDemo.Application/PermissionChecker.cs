using Abp;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo
{
    public class PermissionChecker : IPermissionChecker, ITransientDependency
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        public PermissionChecker()
        {
            AbpSession = NullAbpSession.Instance;
        }
        public async Task<bool> IsGrantedAsync(string permissionName)
        {
            bool result;

            if (permissionName != null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        public Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            throw new NotImplementedException();
        }
    }
}
