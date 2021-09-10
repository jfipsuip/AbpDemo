using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Persons.Dto
{
    public class PersonMapProfile : Profile
    {
        public PersonMapProfile()
        {

            CreateMap<CreatePersonDto, Person>();
            CreateMap<UpdatePersonDto, Person>();

            CreateMap<Person, PersonDto>();
        }
    }
}
