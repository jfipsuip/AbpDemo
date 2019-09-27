using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.SysFileInfos.Dtos
{
    public class FileInfoMapProfile : Profile
    {
        public FileInfoMapProfile()
        {
            CreateMap<FileBaseInfo, FileInfo>()
               .ForMember(d => d.Id, s => s.Ignore());
        }
    }
}
