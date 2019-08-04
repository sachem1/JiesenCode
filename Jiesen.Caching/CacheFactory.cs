using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Caching.Local;
using Jiesen.Caching.Redis;
using Jiesen.Framework.Common;

namespace Jiesen.Caching
{
    public static class CacheFactory
    {
        public static ICache GetLocalCache()
        {
            return LocalCache.Default;
        }

        public static ICache GetRedisCache(string dllName = StaticString.DefaultCacheConfig, string sectionName = StaticString.DefautlRedisConfigSection)
        {
            var setting = RedisManager.GetSetting(dllName, sectionName);
            return GetRedisCache(setting);
        }

        public static ICache GetRedisCache(RedisSetting redisSetting)
        {
            return new RedisCache(redisSetting);
        }
    }
}
