using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Caching
{
    public interface ICache:IDisposable
    {
        T Get<T>(string contextKey, string dataKey, Func<T> action, int? expirationSeonds = null);

        T Get<T>(string contextKey, string dataKey);

        IDictionary<string, T> Get<T>(string contextKey);

        void Remvoe(string contextKey, string dataKey);

        void Remvoe(string contextKey);

        void Set<T>(string contextKey, string dataKey, T value, int? expirationSeconds = null);

        bool Exist(string contextKey, string dataKey);

        bool ExistChildren(string contextKey);
    }
}
