using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jiesen.Core.Extensions;
using Jiesen.Framework.Reflections;

namespace Jiesen.Framework.Autofac
{
    public class IocContainer
    {
        private Dictionary<LifetimeManagerType, Type> LifetimeManager { get; }
        private IocContainer UnityContainer { get; }

        public IocContainer()
        {
            LifetimeManager = new Dictionary<LifetimeManagerType, Type> {
                {LifetimeManagerType.Transient, typeof (TransientLifetimeManager)},
                {LifetimeManagerType.ContainerControlled, typeof (ContainerControlledLifetimeManager)},
                {LifetimeManagerType.Hierarchica, typeof (HierarchicalLifetimeManager)},
                {LifetimeManagerType.Externally, typeof (ExternallyControlledLifetimeManager)},
                {LifetimeManagerType.PerThread, typeof (PerThreadLifetimeManager)},
                {LifetimeManagerType.PerResolve, typeof (PerResolveLifetimeManager)},
                {LifetimeManagerType.PerHttp, typeof (PerThreadLifetimeManager)}
                //{LifetimeManagerType.PerHttp, typeof (PerHttpLifetimeManager)}
            };

            UnityContainer = new UnityContainer();
            BatchInjection();
        }

        private void BatchInjection()
        {
            UnityContainer.AddNewExtension<Interception>();

            var types = MetaDataManager.Type.GetAll().Where(o => o != null && o.IsClass && !o.IsAbstract);
            var map = new ConcurrentDictionary<Type, IEnumerable<InjectAttribute>>();
            Parallel.ForEach(types, item =>
            {
                var registers = item.GetCustomAttributes<InjectAttribute>().ToList();
                if (registers.Count == 0)
                    return;
                map.TryAdd(item, registers);
            });
            map.ForEach(keyValuePair =>
            {
                var item = keyValuePair.Key;
                var registers = keyValuePair.Value;

                foreach (var register in registers)
                {
                    if (register.RegisterType != null)
                        RegisterType(register.Name, register.RegisterType, item,
                            GetLifetimeManager(register.LifetimeManagerType),
                            GetInjectionMembers(register.AopType, item));
                    else
                        RegisterType(register.Name, item,
                            GetLifetimeManager(register.LifetimeManagerType),
                            GetInjectionMembers(register.AopType, item));
                }
            });
        }

        /// <summary>
        /// 获取生命周期
        /// </summary>
        /// <param name="lifetimeManagerType"></param>
        /// <returns></returns>
        public LifetimeManager GetLifetimeManager(LifetimeManagerType lifetimeManagerType)
        {
            UnityContainer.RegisterType<IEventBus, EventBus>(new TransientLifetimeManager());
            Type type;
            if (!LifetimeManager.TryGetValue(lifetimeManagerType, out type))
                return new PerResolveLifetimeManager();
            return (LifetimeManager)Activator.CreateInstance(type);
        }

        /// <summary>
        /// 注入aop方法
        /// </summary>
        /// <param name="aopType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public InjectionMember[] GetInjectionMembers(AopType aopType, Type type)
        {
            var members = new List<InjectionMember>();
            switch (aopType)
            {
                case AopType.VirtualMethodInterceptor:
                    members.Add(new Interceptor<VirtualMethodInterceptor>());
                    break;
                case AopType.InterfaceInterceptor:
                    members.Add(new Interceptor<InterfaceInterceptor>());
                    break;
                case AopType.TransparentProxyInterceptor:
                    members.Add(new Interceptor<TransparentProxyInterceptor>());
                    break;
                case AopType.None:
                    return members.ToArray();
            }
            members.AddRange(type.GetCustomAttributes()
                .Where(item => item.GetType().IsSubclassOf(typeof(UnityAopAttribute)))
                .Cast<UnityAopAttribute>()
                .Select(item => new InterceptionBehavior(item)));

            return members.ToArray();
        }
        
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="injectionMembers"></param>
        /// <param name="name"></param>
        public void RegisterType(string name, Type type, params dynamic[] injectionMembers)
        {
            var members = new List<InjectionMember>();
            LinqExtensions.ForEach(injectionMembers, item =>
            {
                if (item is InjectionMember)
                    members.Add(item);
                if (item is InjectionMember[])
                    members.AddRange(item);
                else if (item is ConstructorParameter)
                    members.Add(new InjectionConstructor(item.Value));
                else if (item is ConstructorParameter[])
                    members.AddRange((item as ConstructorParameter[]).Select(data => new InjectionConstructor(data.Value)));
            });
            var lifetimeManager = injectionMembers.OfType<LifetimeManager>().FirstOrDefault();

            if (string.IsNullOrEmpty(name))
            {
                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType(type);
                else if (lifetimeManager == null)
                    UnityContainer.RegisterType(type, members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType(type, lifetimeManager);
                else
                    UnityContainer.RegisterType(type, lifetimeManager, members.ToArray());
            }
            else
            {
                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType(type, name);
                else if (lifetimeManager == null)
                    UnityContainer.RegisterType(type, name, members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType(type, name, lifetimeManager);
                else
                    UnityContainer.RegisterType(type, name, lifetimeManager, members.ToArray());
            }
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="injectionMembers"></param>
        public void RegisterType(Type type, params dynamic[] injectionMembers)
        {
            var members = new List<InjectionMember>();
            LinqExtensions.ForEach(injectionMembers, item =>
            {
                if (item is InjectionMember)
                    members.Add(item);
                if (item is InjectionMember[])
                    members.AddRange(item);
                else if (item is ConstructorParameter)
                    members.Add(new InjectionConstructor(item.Value));
                else if (item is ConstructorParameter[])
                    members.AddRange((item as ConstructorParameter[]).Select(data => new InjectionConstructor(data.Value)));
            });
            var lifetimeManager = injectionMembers.OfType<LifetimeManager>().FirstOrDefault();

            if (lifetimeManager == null && injectionMembers == null)
                UnityContainer.RegisterType(type);
            else if (lifetimeManager == null)
                UnityContainer.RegisterType(type, members.ToArray());
            else if (injectionMembers == null)
                UnityContainer.RegisterType(type, lifetimeManager);
            else
                UnityContainer.RegisterType(type, lifetimeManager, members.ToArray());
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="injectionMembers"></param>
        public void RegisterType(Type target, Type source, params dynamic[] injectionMembers)
        {
            var members = new List<InjectionMember>();
            LinqExtensions.ForEach(injectionMembers, item =>
            {
                if (item is InjectionMember)
                    members.Add(item);
                if (item is InjectionMember[])
                    members.AddRange(item);
                else if (item is ConstructorParameter)
                    members.Add(new InjectionConstructor(item.Value));
                else if (item is ConstructorParameter[])
                    members.AddRange((item as ConstructorParameter[]).Select(data => new InjectionConstructor(data.Value)));
            });
            var lifetimeManager = injectionMembers.OfType<LifetimeManager>().FirstOrDefault();
            
            if (lifetimeManager == null && injectionMembers == null)
                UnityContainer.RegisterType(target, source);
            else if (lifetimeManager == null)
                UnityContainer.RegisterType(target, source, members.ToArray());
            else if (injectionMembers == null)
                UnityContainer.RegisterType(target, source, lifetimeManager);
            else
                UnityContainer.RegisterType(target, source, lifetimeManager, members.ToArray());
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="target"></param>
        /// <param name="injectionMembers"></param>
        public void RegisterType(string name, Type target, Type source, params dynamic[] injectionMembers)
        {
            var members = new List<InjectionMember>();
            LinqExtensions.ForEach(injectionMembers, item =>
            {
                if (item is InjectionMember)
                    members.Add(item);
                if (item is InjectionMember[])
                    members.AddRange(item);
                else if (item is ConstructorParameter)
                    members.Add(new InjectionConstructor(item.Value));
                else if (item is ConstructorParameter[])
                    members.AddRange((item as ConstructorParameter[]).Select(data => new InjectionConstructor(data.Value)));
            });
            var lifetimeManager = injectionMembers.OfType<LifetimeManager>().FirstOrDefault();


            if (string.IsNullOrEmpty(name))
            {

                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType(target, source);
                else if (lifetimeManager == null)
                    UnityContainer.RegisterType(target, source, members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType(target, source, lifetimeManager);
                else
                    UnityContainer.RegisterType(target, source, lifetimeManager, members.ToArray());

            }
            else
            {
                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType(target, source, name);
                else if (lifetimeManager == null)
                    UnityContainer.RegisterType(target, source, name, members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType(target, source, name, lifetimeManager);
                else
                    UnityContainer.RegisterType(target, source, name, lifetimeManager, members.ToArray());
            }
        }

        /// <summary>
        ///     注册泛型类型
        /// </summary>
        /// <param name="name"></param>
        /// <param name="injectionMembers">构造函数参数</param>
        public void RegisterType<TTarget, TSource>(string name, params dynamic[] injectionMembers) where TSource : TTarget
        {
            var members = new List<InjectionMember>();
            LinqExtensions.ForEach(injectionMembers, item =>
            {
                if (item is InjectionMember)
                    members.Add(item);
                if (item is InjectionMember[])
                    members.AddRange(item);
                else if (item is ConstructorParameter)
                    members.Add(new InjectionConstructor(item.Value));
                else if (item is ConstructorParameter[])
                    members.AddRange((item as ConstructorParameter[]).Select(data => new InjectionConstructor(data.Value)));
            });

            var lifetimeManager = injectionMembers.OfType<LifetimeManager>().FirstOrDefault();
            if (string.IsNullOrEmpty(name))
            {
                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType<TTarget, TSource>();

                else if (lifetimeManager == null)
                    UnityContainer.RegisterType<TTarget, TSource>(members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType<TTarget, TSource>(lifetimeManager);
                else
                    UnityContainer.RegisterType<TTarget, TSource>(lifetimeManager, members.ToArray());

            }
            else
            {
                if (lifetimeManager == null && injectionMembers == null)
                    UnityContainer.RegisterType<TTarget, TSource>(name);

                else if (lifetimeManager == null)
                    UnityContainer.RegisterType<TTarget, TSource>(name, members.ToArray());
                else if (injectionMembers == null)
                    UnityContainer.RegisterType<TTarget, TSource>(name, lifetimeManager);
                else
                    UnityContainer.RegisterType<TTarget, TSource>(name, lifetimeManager, members.ToArray());

            }
        }

        /// <summary>
        ///     注册泛型类型
        /// </summary>
        /// <param name="injectionMembers">构造函数参数</param>
        public void Register<TTarget, TSource>(string name = "", params dynamic[] injectionMembers) where TSource : TTarget
        {
            RegisterType<TTarget, TSource>(name, injectionMembers);
        }

        public void RegisterInstance(Type type, object instance)
        {
            UnityContainer.RegisterInstance(type, instance);
        }

        /// <summary>
        /// 创建泛型实例
        /// </summary>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }

        /// <summary>
        /// 创建泛型实例
        /// </summary>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            return UnityContainer.Resolve<T>(name);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public object Resolve(Type source)
        {
            return UnityContainer.Resolve(source);
        }

        public bool HasRegistered(Type type)
        {
            return UnityContainer.IsRegistered(type);
        }

        public void Release(object obj)
        {
            //todo 这里找不到迁移后的方法
        }
    }
}
