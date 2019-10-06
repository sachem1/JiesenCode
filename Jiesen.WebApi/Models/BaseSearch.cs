using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models
{
    public class BaseSearch
    {
        public int SkipCount { get; set; }

        public int MaxResultCount { get; set; }

    }
}