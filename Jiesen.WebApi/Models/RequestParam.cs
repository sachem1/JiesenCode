using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models
{
    public class RequestParam
    {
        public IEnumerable<long> ids { get; set; }
    }
}