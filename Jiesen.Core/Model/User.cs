using System;
using System.Collections.Generic;
using System.Text;

namespace Jiesen.Core.Model
{
    public class User : Entity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public int Age { get; set; }
    }
}
