
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AbpDemo.SysAreas.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.SysAreas
{
    /// <summary>
    /// 区划管理
    /// </summary>
    public class SysAreaService : AsyncCrudAppService<SysArea, SysAreaDto, Guid, PagedAndSortedResultRequestDto, SysAreaCDto, SysAreaUDto, EntityDto<Guid>, EntityDto<Guid>>, ISysAreaService
    {

        public SysAreaService(IRepository<SysArea, Guid> repository) : base(repository)
        {

        }
    }
}
