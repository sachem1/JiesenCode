using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jiesen.Caching.Test.Cache
{
    [TestClass]
    public class RedisCacheTest
    {
        private ICache _redisCache=CacheFactory.GetRedisCache();
        public string contextKey = "redis";
        
        [TestMethod]
        public void Redis_Get_Test()
        {
            var value = "123456";
            var dataKey = nameof(Redis_Get_Test);
            _redisCache.Set(contextKey, dataKey, value, 360);
            var result=_redisCache.Get<string>(contextKey,dataKey );
            Assert.AreEqual(result,value);
        }



        [TestMethod]
        public void Redis_Set_Test()
        {
            var value = "123456";
            var dataKey = nameof(Redis_Set_Test);
            _redisCache.Set(contextKey, dataKey, value, 360);
            var result = _redisCache.Get<string>(contextKey, dataKey);
            Assert.AreEqual(result, value);
        }


        [TestMethod]
        public void Redis_Cache_Expire_Test()
        {
            var value = "123456";
            var dataKey = nameof(Redis_Set_Test);
            _redisCache.Set(contextKey, dataKey, value, 3);
            Thread.Sleep(5000);
            var result = _redisCache.Get<string>(contextKey, dataKey);
            Assert.IsNull(result);
        }
    }
}