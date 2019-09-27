using Abp;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using AbpDemo.SysFileInfos.Dtos;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AbpDemo.SysFileInfos
{
    public class SysFileInfoAppService : AsyncCrudAppService<FileInfo, FileInfoDto, Guid>
    {
        public SysFileInfoAppService(IRepository<FileInfo, Guid> repository) : base(repository)
        {
        }

        public void SaveData(string path)
        {
            string s = File.ReadAllText(path);
            var fileBaseInfos = JsonConvert.DeserializeObject<List<FileBaseInfo>>($"[{s.Trim(',')}]");
            List<FileInfo> fileinfos = fileBaseInfos.Select(t => Mapper.Map<FileInfo>(t)).ToList();
            foreach (var item in fileinfos)
            {
                Repository.Insert(item);
            }
        }
    }
}
