using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Areas
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class Area : Entity<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public string AreaCode { get; set; }
    }
}
