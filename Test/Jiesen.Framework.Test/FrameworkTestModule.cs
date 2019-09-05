using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Core.Modules;
using Jiesen.EntityFramework;
using Jiesen.Framework.Modules;

namespace Jiesen.Framework.Test
{
    [DependsOn(typeof(EntityFrameworkModule),typeof(FrameworkModule))]
    public class FrameworkTestModule:JiesenModule
    {

    }
}
