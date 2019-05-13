using Abp.Domain.Entities;
using AbpDemo.Areas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbpDemo.Depts
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Dept : Entity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 行政区划Id
        /// </summary>
        public Guid? AreaId { get; set; }

        /// <summary>
        /// 行政区划
        /// </summary>
        [ForeignKey(nameof(AreaId))]
        public virtual Area Area { get; set; }
    }
}
