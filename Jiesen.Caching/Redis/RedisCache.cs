using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Jiesen.Caching.Redis
{
    public class RedisCache:BaseCache,ICache
    {
        private readonly IDatabase _db;

        public RedisCache(RedisSetting redisSetting)
        {
            _db= RedisManager.GetClient(redisSetting);
        }

        public T Get<T>(string contextKey, string dataKey, Func<T> action, int? expirationSeconds = null)
        {
            T RedisAction()
            {
                var key = GeneralContextKey(contextKey);
                if (_db.HashExists(key, dataKey))
                {
                    return ResolveJson<T>(_db.HashGet(key, dataKey));
                }
                T data = action();
                if (data == null)
                    throw new NullReferenceException("缓存中添加的数据为null");
                _db.HashSet(key, dataKey, JsonConvert.SerializeObject(data));
                if (expirationSeconds.HasValue && expirationSeconds != 0)
                    _db.KeyExpire(key, TimeSpan.FromSeconds(expirationSeconds.Value));
                return data;
            }

            return RedisAction();
        }

        public T Get<T>(string contextKey, string dataKey)
        {
            var key = GeneralContextKey(contextKey);
            return ResolveJson<T>(_db.HashGet(key,dataKey));

        }

        public IDictionary<string, T> Get<T>(string contextKey)
        {
            var key = GeneralContextKey(contextKey);
            return _db.HashGetAll(key).ToDictionary(item => (string)item.Name, item => ResolveJson<T>(item.Value));
        }

        public void Remvoe(string contextKey, string dataKey)
        {
            var key = GeneralContextKey(contextKey);
            if (_db.HashExists(key, dataKey))
            {
                _db.HashDelete(key, dataKey);
            }
        }

        public void Remvoe(string contextKey)
        {
            var key = GeneralContextKey(contextKey);
            _db.KeyDelete(key);
        }

        public void Set<T>(string contextKey, string dataKey, T value, int? expirationSeconds = null)
        {
            var key = GeneralContextKey(contextKey);
            _db.HashSet(key, dataKey, JsonConvert.SerializeObject(value));
            if (expirationSeconds.HasValue && expirationSeconds!=0)
            {
                _db.KeyExpire(key, TimeSpan.FromSeconds(expirationSeconds.Value));
            }

        }

        public bool Exist(string contextKey, string dataKey)
        {
            if (string.IsNullOrEmpty(contextKey) || string.IsNullOrEmpty(dataKey))
                return false;
            var key = GeneralContextKey(contextKey);
            return _db.HashExists(key, dataKey);
        }

        public bool ExistChildren(string contextKey)
        {
            if (string.IsNullOrEmpty(contextKey))
                return false;
            var key = GeneralContextKey(contextKey);
            //todo 这里的判断带验证,skd的方法是否提供通配符获取
            return _db.KeyExists(key);
        }

        public void Dispose()
        {
            
        }


        
        /// <summary>
        /// 解析redisvalue，包含json解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T ResolveJson<T>(RedisValue value)
        {
            if (!value.HasValue)
                return default(T);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
