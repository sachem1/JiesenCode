using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jiesen.Contract
{
    public class Person:BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public byte Gender { get; set; }
    }
}
