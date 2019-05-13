using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AbpDemo.Areas.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.Areas
{
    public class AreaService : AsyncCrudAppService<Area, AreaDto, Guid, PagedAndSortedResultRequestDto, CreateAreaDto, UpdateAreaDto, EntityDto<Guid>, EntityDto<Guid>>
    {
        public AreaService(IRepository<Area, Guid> repository) : base(repository)
        {
        }
    }
}
