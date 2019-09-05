using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Framework.Contexts
{
    public abstract class Context : IDisposable
    {
        /// <summary>
        /// 销毁的时候发出的事件
        /// </summary>
        internal event Action<Context> Dispised;

        public void Dispose()
        {
            Dispised?.Invoke(this);
        }
    }
}
