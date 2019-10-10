using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.ConsoleApp
{
    public class TestConnection
    {
        private static object conn;

        public object GetConnnection()
        {
            if (conn != null) return conn;
            conn = new { Name = DateTime.Now.ToString() };
            return conn;
        }
    }
}
