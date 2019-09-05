using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Caching;
using Jiesen.Core.Common;
using Jiesen.Core.Models;

namespace Jiesen.Framework.Contexts
{
    public class PlatformContext : Context
    {
        public PlatformContext(string name, ICache cache) { }
        public static PlatformContext Current { get; set; }

        public UserContext UserContext { get; set; }

        public AppContext CreateAppContext(AppModel app)
        {
            //AppContext.Current = new AppContext(this, app, CacheFactory.GetRedisCache(sectionName: StaticString.AppContextString));
            AppContext.Current.App = app;
            return AppContext.Current;
        }

    }
}
