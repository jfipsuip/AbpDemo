using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.SysAreas.Dtos
{
    public class SysAreaMapProfile : Profile
    {
        public SysAreaMapProfile()
        {
            CreateMap<SysAreaCDto, SysArea>();
            CreateMap<SysAreaUDto, SysArea>();
            CreateMap<SysArea, SysAreaDto>();
        }
    }
}
