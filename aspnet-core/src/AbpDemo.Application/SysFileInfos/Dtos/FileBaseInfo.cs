using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.SysFileInfos.Dtos
{
    public class FileBaseInfo
    {
        public int Id { get; set; }
        public DateTime Now { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Length { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public string SHA1 { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
