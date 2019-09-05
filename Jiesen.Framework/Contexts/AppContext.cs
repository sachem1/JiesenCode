using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Caching;
using Jiesen.Core.Common;
using Jiesen.Core.ConfigManager;
using Jiesen.Core.Models;

namespace Jiesen.Framework.Contexts
{
    public class AppContext:Context
    {
        public AppContext(ICache cache)
        {
            Cache = cache;
        }

        AppContext() { }


        public PlatformContext PlatformContext { get; set; }


        protected int TimeOut { get;  }= 3600;

        public static AppContext Current { get; set; } = new AppContext();

        public AppModel App { get; set; } = new AppModel();

    }
}
