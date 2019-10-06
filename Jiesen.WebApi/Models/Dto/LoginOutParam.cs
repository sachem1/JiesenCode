using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models.Dto
{
    public class LoginOutParam
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}