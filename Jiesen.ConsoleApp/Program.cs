using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Jiesen.Caching;
using Jiesen.Component.Contract;
using Jiesen.Contract;
using Jiesen.EntityFramework;
using Jiesen.Component.Service;
using CacheFactory = CacheManager.Core.CacheFactory;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Jiesen.ConsoleApp
{
    class Program
    {
        private static IContainer _container;

        static ConcurrentDictionary<string,string> testTasks=new ConcurrentDictionary<string, string>();
        static void Main(string[] args)
        {

            //var stri = JsonConvert.SerializeObject(new { Name="张三",Age=18});

            //using (JiesenDbContext jiesenDbContext = new JiesenDbContext())
            //{
            //    Person person = new Person() { Name = "test" };
            //    jiesenDbContext.Persons.Add(person);
            //    jiesenDbContext.SaveChanges();

            //    var result = jiesenDbContext.Persons.Select(x => x.Name.Contains("te"));
            //}
            //{
            //testTasks.AddOrUpdate("123", ()=>"","");

            //Console.WriteLine(Math.Abs(2.3));
            //Console.WriteLine(Math.Abs(-2.1));
            //Console.WriteLine(Math.Abs(-2.0));
            //_container = ConfigureDependencies();
            //{
            //    var testService = _container.Resolve<ITestService>();
            //    var result = testService.Calculate(2, 3);
            //    Console.WriteLine(result);
            //}
            //var localCache = _container.Resolve<ICache>();
            //localCache.Set("1","2","3");
            //var cacheResult = localCache.Get<string>("1", "2");
            //Console.WriteLine(cacheResult);

            //using (var container = _container.BeginLifetimeScope("Cache"))
            //{
            //    var redisCache = container.Resolve<ICache>();
            //    redisCache.Set("redis","1","2");

            //    Console.WriteLine(redisCache.Get<string>("redis", "1"));
            //}

            //_container.BeginLifetimeScope("Cache");



            //}
            //{
            //    CacheTest();
            //}
            //{
            //var ss = Bakversion();
            //Console.WriteLine(ss);
            //}


            //var localCache = Jiesen.Caching.CacheFactory.GetLocalCache();
            //localCache.Set("","test","123456");
            //Console.WriteLine(localCache.Get<string>("", "test"));

            //var redisCache = Jiesen.Caching.CacheFactory.GetRedisCache();
            //redisCache.Set("","one","456789");

            //Console.WriteLine(redisCache.Get<string>("", "one"));

            //AutofacTest();
            TryCatch();
            NoTryCatch();
            Console.ReadLine();
        }
        static int total = 100000;
        private static void NoTryCatch()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            for (int i = 0; i < total; i++)
            {
                var str = "{\"Name\":\"张三\",\"Age\":18}";
                var obj = JsonConvert.DeserializeObject(str);

            }
            Console.WriteLine($"{total}次无tryCatch耗时{stopwatch.ElapsedMilliseconds} ms");        

        }
        private static void TryCatch()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            stopwatch.Start();
            for (int i = 0; i < total; i++)
            {
                try
                {

                    var str = "{\"Name\":\"张三\",\"Age\":18}";
                    var obj = JsonConvert.DeserializeObject(str);
                }
                catch (Exception)
                {
                    throw;
                }

            }
            Console.WriteLine($"{total}次tryCatch耗时{stopwatch.ElapsedMilliseconds} ms");
        }

        private static string Bakversion()
        {
            var bakversion = "19050402";
            var version = "19050303";


            var val1 = bakversion.Substring(0, 6);
            var val2 = version.Substring(0, 6);
            if (val1 == val2)
            {
                var k1 = Convert.ToInt32(bakversion.Substring(6, 2));
                var k2 = Convert.ToInt32(version.Substring(6, 2));
                if (k1 > k2)
                {
                    return bakversion;
                }
            }
            else
            {
                var t1 = DateTime.ParseExact(val1, "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                var t2 = DateTime.ParseExact(val2, "yyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                if (t1 > t2)
                    return bakversion;
            }
            return version;
        }

        private static IContainer ConfigureDependencies()
        {
            // Register default dependencies in the application container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new CacheModule());
            _container = builder.Build();
            return _container;
        }

        private static void disPlayPath()
        {
            var path1 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var path2 = System.AppDomain.CurrentDomain.BaseDirectory;
            var path3 = System.Environment.CurrentDirectory;
            //var path4 = System.Windows.Forms.Application.StartupPath;
        }

        private static void CacheTest()
        {
            var cache = CacheFactory.Build("test", setting => setting.WithDictionaryHandle(true));
            cache.Add("dd", "123");
            Thread.Sleep(1200);
            var result = cache.Get("dd");
            Console.WriteLine(result);
        }

        private static void AutofacTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    // Every one of the 100 Worker instances
                    // resolved in this loop will be brand new.
                    var w = scope.Resolve<ITestService>();
                    //Console.WriteLine(w.Calculate(i , i + 1));
                    Console.WriteLine(w.GetHashCode());
                }
            }
        }
    }
}
