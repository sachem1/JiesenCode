using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Caching
{
    public abstract class BaseCache
    {
        protected string GeneralContextKey(string contextKey)
        {
            return $"jiesen.{contextKey}";
        }
    }
}
