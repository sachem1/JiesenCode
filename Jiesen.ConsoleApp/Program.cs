using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Jiesen.Component.Contract;
using Jiesen.Component.Service;

namespace Jiesen.ConsoleApp
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            _container = ConfigureDependencies();

            var testService = _container.Resolve<ITestService>();
            var result = testService.Calculate(2, 3);
            Console.WriteLine(result);
            Console.ReadLine();

        }

        private static IContainer ConfigureDependencies()
        {
            // Register default dependencies in the application container.
            var builder = new ContainerBuilder();
            // zhe li xuyao  xiayi dao bie de difang
            builder.RegisterModule(new ServiceModule());
            _container = builder.Build();
            return _container;
        }
    }
}
