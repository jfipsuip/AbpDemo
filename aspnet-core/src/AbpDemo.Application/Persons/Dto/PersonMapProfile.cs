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

            CreateMap<Person, StudentDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(t => t.FirstName + " " + t.LastName))
                .ForMember(d => d.IsAdult, opt => opt.MapFrom(t => t.Age >= 18 ? true : false));
        }
    }
}
