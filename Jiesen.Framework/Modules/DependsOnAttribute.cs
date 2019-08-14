using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Framework.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute:Attribute
    {
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// 用于定义模块与其他模块的依赖关系。
        /// </summary>
        /// <param name="dependedModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
