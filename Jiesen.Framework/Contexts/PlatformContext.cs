using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Framework.Contexts
{
    public class PlatformContext : Context
    {
        public static PlatformContext Current { get; set; }

        public UserContext UserContext { get; set; }
    }
}
