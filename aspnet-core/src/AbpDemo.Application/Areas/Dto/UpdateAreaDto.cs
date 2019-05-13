using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Areas.Dto
{
    public class UpdateAreaDto : EntityDto<Guid>
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
