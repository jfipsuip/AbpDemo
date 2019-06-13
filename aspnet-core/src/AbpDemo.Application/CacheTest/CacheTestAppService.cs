using Abp.Application.Services;
using Abp.Extensions;
using Abp.Runtime.Caching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbpDemo.CacheTest
{
    public class CacheTestAppService : ApplicationService
    {
        public ICacheManager cacheManager { get; set; }
        public void SetCache(string ticket, string value)
        {
            TimeSpan expireTime = TimeSpan.FromSeconds(100);
            cacheManager.GetCache("TestCache").Set(ticket, value, expireTime);
        }
        public string GetCache(string ticket)
        {
            var cachemodel = cacheManager.GetCache("TestCache").Get(ticket, null).As<string>();
            return cachemodel;
        }
        public string GetCacheDefault(string ticket)
        {
            var cachemodel = cacheManager.GetCache("TestCache").Get(ticket, () => GetDefault()).As<string>();
            return cachemodel;
        }

        private static string GetDefault()
        {
            return $"没有值，数据是在{DateTime.Now}默认设置的";
        }
    }
}
