using System;
using System.Collections.Generic;

namespace Jiesen.Core.Modules
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public interface IModuleManager
    {
        /// <summary>
        /// 根据模块中的某个类型获取模块
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        JiesenModule GetModule(Type type);

        /// <summary>
        /// 根据模块名字获取
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        JiesenModule GetModule(string moduleName);

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        JiesenModule GetModule(Func<JiesenModule, bool> predicate);

        /// <summary>
        /// 获取多个模块
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<JiesenModule> GetModules(Func<JiesenModule, bool> predicate);

        /// <summary>
        /// 加载所有的模块
        /// </summary>
        void InstallModules();

        /// <summary>
        /// 加载所有的模块
        /// </summary>
        /// <param name="startupModule">启动模块</param>
        void InstallModules(Type startupModule);

        /// <summary>
        /// 加载所有的模块
        /// </summary>
        /// <param name="startupModule">启动模块</param>
        /// <param name="moduleTypes"></param>
        void InstallModules(Type startupModule, IEnumerable<Type> moduleTypes);

        /// <summary>
        /// 卸载所有的module
        /// </summary>
        void UninstallModules();
    }
}
