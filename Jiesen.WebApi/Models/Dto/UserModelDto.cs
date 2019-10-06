using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models.Dto
{
    public class UserModelDto : BaseModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }
    }
}