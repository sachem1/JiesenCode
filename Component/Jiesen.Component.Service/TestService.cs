using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jiesen.Component.Contract;

namespace Jiesen.Component.Service
{
    public class TestService: ITestService
    {
        public int Calculate(int a, int b)
        {
            return a * b;
        }
    }
}
