using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.SysAreas
{
    /// <summary>
    /// 行政区划
    /// </summary>
    public class SysArea : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 排序
        /// </summary>
        public Nullable<int> AreaSort { set; get; }

        /// <summary>
        /// 区划照片
        /// </summary>
        public string AreaPhotoUrl { set; get; }

        /// <summary>
        /// 区划状态
        /// </summary>
        public Nullable<int> AreaStatus { set; get; }

        /// <summary>
        /// 区划描述
        /// </summary>
        public string AreaRemark { set; get; }

        /// <summary>
        /// 区划类型
        /// </summary>
        public string AreaType { set; get; }

        /// <summary>
        /// 区划面积
        /// </summary>
        public Nullable<decimal> AreaZone { set; get; }

        /// <summary>
        /// 区划等级
        /// </summary>
        public string AreaLevel { set; get; }

        /// <summary>
        /// 区划编码
        /// </summary>
        public string AreaCode { set; get; }

        /// <summary>
        /// 区划名称
        /// </summary>
        public string AreaName { set; get; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Nullable<Guid> PId { set; get; }
    }

}
