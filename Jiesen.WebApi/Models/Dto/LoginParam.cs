using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models.Dto
{
    public class LoginParam
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string ChildrenSystem { get; set; }

    }
}