using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AbpDemo.Areas.Dto;
using AbpDemo.Persons.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.Persons
{
    public class PersonService : AsyncCrudAppService<Person, PersonDto, Guid, PagedAndSortedResultRequestDto, CreatePersonDto, UpdatePersonDto, EntityDto<Guid>, EntityDto<Guid>>
    {
        public PersonService(IRepository<Person, Guid> repository) : base(repository)
        {
        }
        public async Task<StudentDto> GetStudent(EntityDto<Guid> entityDto)
        {
            Person entity = await Repository.GetAsync(entityDto.Id);

            StudentDto student = AutoMapper.Mapper.Map<StudentDto>(entity);

            return student;
        }
    }
}
