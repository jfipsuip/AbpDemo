using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Persons.Dto
{
    public class StudentDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Sex { get; set; }

        public bool? IsAdult { get; set; }
    }
}
