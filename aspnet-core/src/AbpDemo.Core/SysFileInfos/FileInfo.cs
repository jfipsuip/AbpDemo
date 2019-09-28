using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AbpDemo.SysFileInfos
{
    public class FileInfo : Entity<Guid>
    {
        [StringLength(300)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Extension { get; set; }
        public long Length { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        [StringLength(50)]
        public string SHA1 { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}
