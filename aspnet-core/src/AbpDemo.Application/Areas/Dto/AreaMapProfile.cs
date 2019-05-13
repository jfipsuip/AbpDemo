using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Areas.Dto
{
    public class AreaMapProfile : Profile
    {
        public AreaMapProfile()
        {

            CreateMap<CreateAreaDto, Area>();
            CreateMap<UpdateAreaDto, Area>();

            CreateMap<Area, AreaDto>();
        }
    }
}
