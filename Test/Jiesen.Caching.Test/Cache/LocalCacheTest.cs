using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jiesen.Caching.Test.Cache
{
    [TestClass]
    public class LocalCacheTest
    {
        private readonly ICache _cache = CacheFactory.GetLocalCache();
        private string contextKey = "cache";
        [TestMethod]
        public void LocalCache_Get_Test()
        {
            _cache.Set(contextKey, nameof(LocalCache_Get_Test), nameof(LocalCache_Get_Test));
            Assert.IsNotNull(_cache.Get<string>(contextKey, nameof(LocalCache_Get_Test)));
        }

        [TestMethod]
        public void LocalCache_Get_Expirtd_Test()
        {
            _cache.Set(contextKey, nameof(LocalCache_Get_Expirtd_Test), nameof(LocalCache_Get_Expirtd_Test), 2);
            Thread.Sleep(4000);
            Assert.IsNull(_cache.Get<string>(contextKey, nameof(LocalCache_Get_Expirtd_Test)));
        }

        [TestMethod]
        public void Exist_Key_Test()
        {
            _cache.Set(contextKey, nameof(Exist_Key_Test), nameof(Exist_Key_Test), 30);
            Assert.IsTrue(_cache.Exist(contextKey, nameof(Exist_Key_Test)));
        }

        [TestMethod]
        public void Remove_Cache_Test()
        {
            _cache.Set(contextKey,nameof(Remove_Cache_Test),nameof(Remove_Cache_Test));
            Assert.IsTrue(_cache.Exist(contextKey,nameof(Remove_Cache_Test)));
            _cache.Remvoe(contextKey,nameof(Remove_Cache_Test));
            Assert.IsNull(_cache.Get<string>(contextKey,nameof(Remove_Cache_Test)));
        }
    }
}
