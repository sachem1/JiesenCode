using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Core.Common;
using Jiesen.Core.ConfigManager;

namespace Jiesen.Caching.Local
{
    public class LocalCache : BaseCache,ICache
    {
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly int _timeOut;
        internal static readonly ICache Default;
        public LocalCache()
        {
            const string local = "Local";
            const string timeout = "Timeout";
            if (AppConfig.IsExists(StaticString.DefaultCacheConfig) &&
                AppConfig.DllConfigs[StaticString.DefaultCacheConfig].IsExists(local) &&
                AppConfig.DllConfigs[StaticString.DefaultCacheConfig][local].IsExists(timeout))
                _timeOut = JiesenConvert.Convert<int>(AppConfig.DllConfigs[StaticString.DefaultCacheConfig][local][timeout]);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="contextKey">程序上下文</param>
        /// <param name="dataKey">缓存key</param>
        /// <param name="action"></param>
        /// <param name="expirationSeonds">超时时间</param>
        /// <returns></returns>
        public T Get<T>(string contextKey, string dataKey, Func<T> action, int? expirationSeonds = null)
        {
            if (Exist(contextKey, dataKey))
            {
                return Get<T>(contextKey, dataKey);
            }
            var value = action();
            if (value == null) return default(T);
            Set(contextKey, dataKey, value, expirationSeonds);
            return value;
        }

        public T Get<T>(string contextKey, string dataKey)
        {
            var key = GeneralContextKey(contextKey);
            if (!(_cache.Get(key) is ConcurrentDictionary<string, object> hset))
            {
                return default(T);
            }
            if (hset.TryGetValue(dataKey, out var value))
            {
                return (T)value;
            }
            return default(T);
        }

        public IDictionary<string, T> Get<T>(string contextKey)
        {
            var key = GeneralContextKey(contextKey);
            if (_cache.Get(key) is ConcurrentDictionary<string, object> hset)
            {
                return hset.ToDictionary(item => item.Key, item => (T)item.Value);
            }
            return new ConcurrentDictionary<string, T>();
        }

        public void Remvoe(string contextKey, string dataKey)
        {
            var key = GeneralContextKey(contextKey);
            if (!(_cache.Get(key) is ConcurrentDictionary<string, object> hset))
                return;
            hset.TryRemove(dataKey, out _);
        }

        public void Remvoe(string contextKey)
        {
            var key = GeneralContextKey(contextKey);
            if (_cache.Contains(key))
                _cache.Remove(key);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Set<T>(string contextKey, string dataKey, T value, int? expirationSeconds = null)
        {
            var key = GeneralContextKey(contextKey);
            int seconds = expirationSeconds ?? _timeOut;

            if (!_cache.Contains(key))
            {
                var hset = new ConcurrentDictionary<string, object>(new Dictionary<string,object>(){{ dataKey, value } });
                _cache.Set(key,hset,DateTimeOffset.Now.AddSeconds(seconds));
            }
            else
            {
                var hset = _cache.Get(key) as ConcurrentDictionary<string,object>;
                hset?.AddOrUpdate(dataKey, k => value, (k, oldvalue) => value);
            }
        }

        public bool Exist(string contextKey, string dataKey)
        {
            var key = GeneralContextKey(contextKey);
            return _cache.Get(key) is ConcurrentDictionary<string, object> hset && hset.ContainsKey(dataKey);
        }

        public bool ExistChildren(string contextKey)
        {
            return _cache.Contains(GeneralContextKey(contextKey));
        }

        public void Dispose()
        {

        }

    }
}
