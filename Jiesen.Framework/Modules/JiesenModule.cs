using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Framework.Modules
{
    public abstract class JiesenModule
    {
        /// <summary>
        ///这是应用程序启动时调用的第一个事件。
        ///在此处放置代码以在依赖注入注册之前运行。
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        ///此方法用于注册此模块的依赖项。
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 在应用程序启动时调用此方法。
        /// </summary>
        public virtual void PostInitialize()
        {

        }

        /// <summary>
        /// 在关闭应用程序时调用此方法
        /// </summary>
        public virtual void Shutdown()
        {

        }

        public virtual Assembly[] GetAdditionalAssemblies()
        {
            return new Assembly[0];
        }

        /// <summary>
        /// 检查给定类型是否为基模块类。
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsJiesenModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(JiesenModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// 查找模块的直接依赖模块（不包括给定模块）。
        /// </summary>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsJiesenModule(moduleType))
            {
                throw new Exception("此类型不是jiesen模块: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, moduleType);
            //list.AddIfNotContains(typeof(AbpKernelModule));
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
        {
            if (!IsJiesenModule(module))
            {
                throw new Exception("This type is not an ABP module: " + module.AssemblyQualifiedName);
            }

            if (modules.Contains(module))
            {
                return;
            }

            modules.Add(module);

            var dependedModules = FindDependedModuleTypes(module);
            foreach (var dependedModule in dependedModules)
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            }
        }
    }
}
