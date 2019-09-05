using System;
using System.Linq;
using Jiesen.Core.Modules;
using Jiesen.Framework.Contexts;
using Jiesen.Framework.Reflections;

namespace Jiesen.Framework.Modules
{
    public static class ModuleExtensions
    {
       
        public static AppContext LoadModules(this AppContext context, Type startupModule)
        {
            var modules = JiesenModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(startupModule).Distinct();
            MetaDataManager.Type.Register(modules);
            //IModuleManager module = UnityService.Resolve<IModuleManager>();
            //module.InstallModules(startupModule, modules);
            
            return context;
        }
    }
}
