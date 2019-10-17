using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CacheManager.Core;
using Jiesen.Component.Contract;
using Jiesen.Contract;
using Jiesen.EntityFramework;using Jiesen.Component.Service;
using Newtonsoft.Json;

namespace Jiesen.ConsoleApp
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            //using (JiesenDbContext jiesenDbContext = new JiesenDbContext())
            //{
            //    Person person = new Person() { Name = "test" };
            //    jiesenDbContext.Persons.Add(person);
            //    jiesenDbContext.SaveChanges();

            //    var result = jiesenDbContext.Persons.Select(x => x.Name.Contains("te"));
            //}
            //{
            //    _container = ConfigureDependencies();
            //    var testService = _container.Resolve<ITestService>();
            //    var result = testService.Calculate(2, 3);
            //    Console.WriteLine(result);
            //}
            {
               // CacheTest();
            }
            {
                //var ss = Bakversion();
                //Console.WriteLine(ss);
            }
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    Task.Factory.StartNew(() =>
                //    {
                //        var obj = TestConnection.GetConnnection();
                //        Console.WriteLine("当前连接:" + JsonConvert.SerializeObject(obj));
                //    });
                //}               
            }

            Console.ReadLine();
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
    }
}
