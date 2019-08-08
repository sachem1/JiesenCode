
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Jiesen.Caching.Test
{
    public class CacheTestModule:Module
    {
        public IComponentContext ComponentContext;

        public CacheTestModule(IComponentContext componentContext)
        {
            ComponentContext = componentContext;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CacheModule>();
        }
    }
}
