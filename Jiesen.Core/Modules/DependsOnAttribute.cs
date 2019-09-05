using System;

namespace Jiesen.Core.Modules
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
