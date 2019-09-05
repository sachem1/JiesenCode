using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Jiesen.Core.Extensions;

namespace Jiesen.Framework.Reflections
{
    public class AssemblyFinder
    {
        private List<Assembly> _referencedAssemblies;
        public List<Assembly> GetAllAssemblies()
        {
            return ReferencedAssemblies;
        }

        public string GetAssemblySortName(Assembly assembly)
        {
            AssemblyName name = new AssemblyName(assembly.FullName);
            return name.Name;
        }


        /// <summary>
        /// 加载所有的程序集
        /// </summary>
        public List<Assembly> ReferencedAssemblies
        {
            get
            {
                if (_referencedAssemblies != null)
                    return _referencedAssemblies;

                Func<Assembly, bool> filter = assembly =>
                {
                    if (assembly == null)
                        return false;
                    if (assembly.FullName.StartsWith("mscorlib") ||
                        assembly.FullName.StartsWith("System") ||
                        assembly.FullName.StartsWith("Microsoft") ||
                        assembly.FullName.StartsWith("am.Charts") ||
                        assembly.FullName.StartsWith("ComponentArt") ||
                        assembly.FullName.StartsWith("Syncfusion") ||
                        assembly.FullName.StartsWith("LumiSoft") ||
                        assembly.FullName.StartsWith("Newtonsoft") ||
                        assembly.FullName.StartsWith("MySql") ||
                        assembly.FullName.StartsWith("Krystalware") ||
                        assembly.FullName.StartsWith("HtmlAgilityPack") ||
                        assembly.FullName.StartsWith("Ionic") ||
                        assembly.FullName.StartsWith("ICSharpCode") ||
                        assembly.FullName.StartsWith("eWebEditor") ||
                        assembly.FullName.StartsWith("App_Code") ||
                        assembly.FullName.StartsWith("App_global") ||
                        assembly.Location.StartsWith(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "obj")))
                        return false;
                    return true;
                };
                _referencedAssemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.*", SearchOption.AllDirectories)
                    .Where(item => item.EndsWith(".dll") || item.EndsWith(".exe"))
                    .Select(item =>
                    {
                        try
                        {
                            return Assembly.LoadFrom(item);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    })
                    .Where(filter)
                    .DistinctBy(item => item.FullName)
                    .ToList();
                return _referencedAssemblies;
            }
        }

        /// <summary>
        /// 注册特定目录依赖程序集
        /// </summary>
        public void RegisterDependencyAssemblyResolveFromPlugins()
        {
            string basePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "plugins");
            var assemblies = Directory.GetFiles(basePath, $"*.dll", SearchOption.AllDirectories)
                .Select(item =>
                {
                    return Assembly.LoadFrom(item);
                });
            _referencedAssemblies.AddRange(assemblies);
            //AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            //{
            //    if (args.RequestingAssembly == null)
            //    {
            //        string basePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "plugins");
            //        string name = args.Name.Substring(0, args.Name.IndexOf(",", StringComparison.Ordinal));
            //        var dlls = System.IO.Directory.GetFiles(basePath, $"{name}.dll", SearchOption.AllDirectories);
            //        if (dlls.Length > 0)
            //            return Assembly.LoadFrom(dlls[0]);
            //    }
            //    return args.RequestingAssembly;
            //};
        }
    }
}
