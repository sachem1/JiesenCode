using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Jiesen.Framework.Logs;

namespace Jiesen.Framework.Reflections
{
    public class TypeFinder
    {
        private List<Type> _allTypes;
        private IEnumerable<Assembly> _moduleAssemblies;

        public IEnumerable<Type> GetCurrentAssemblyTypes()
        {
            return Assembly.GetCallingAssembly().GetTypes();
        }

        public IEnumerable<Type> Find(Func<Type, bool> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IEnumerable<Type> GetAll()
        {
            if (_allTypes != null)
                return _allTypes;
            _allTypes = new List<Type>();
            foreach (var assembly in MetaDataManager.Assembly.ReferencedAssemblies)
            {
                try
                {
                    Type[] typesInThisAssembly;
                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly == null || typesInThisAssembly.Length == 0)
                    {
                        continue;
                    }
                    _allTypes.AddRange(typesInThisAssembly.Where(type => type != null));
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Warning(ex.ToString(), ex);
                }
            }
            return _allTypes;
        }

        internal void Register(IEnumerable<Type> moduleTypes)
        {
            if (_allTypes == null)
            {
                var temp = new List<Type>();
                Interlocked.CompareExchange(ref _allTypes, temp, null);
            }

            var assemblies = moduleTypes.Select(o => o.Assembly).Distinct();
            _moduleAssemblies = assemblies;
            Parallel.ForEach(assemblies, (assembly) =>
            {
                try
                {
                    Type[] typesInThisAssembly;
                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly == null || typesInThisAssembly.Length == 0)
                    {
                        return;
                    }

                    var cleanedTypes = typesInThisAssembly.Where(t => t != null &&
                                                                      !_allTypes.Contains(t));
                    lock (_allTypes)
                    {
                        _allTypes.AddRange(cleanedTypes);
                    }
                }
                catch (Exception ex)
                {
                    LoggerFactory.Instance.Warning(ex.ToString(), ex);
                }
            });
        }

        public IEnumerable<Assembly> GetModuleAssemblies()
        {
            if (_moduleAssemblies == null || !_moduleAssemblies.Any())
                //throw CoralException.ThrowException(item => item.ModuleError, "GetAll(IList<Type> moduleTypes)未被调用过。");
                throw new Exception("GetAll(IList<Type> moduleTypes)未被调用过。");

            return _moduleAssemblies;
        }
    }
}
