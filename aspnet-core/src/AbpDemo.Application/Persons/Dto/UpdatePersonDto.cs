using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Persons.Dto
{
    public class UpdatePersonDto : EntityDto<Guid>
    {
        public string Address { get; set; }
    }
}
