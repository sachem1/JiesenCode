using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Jiesen.Caching.Local;
using Jiesen.Caching.Redis;

namespace Jiesen.Caching
{
    public class CacheModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalCache>().As<ICache>().InstancePerDependency();

            builder.RegisterType(typeof(RedisSetting));
            builder.RegisterType<RedisCache>().As<ICache>().InstancePerMatchingLifetimeScope("Cache");
        }
    }
}
