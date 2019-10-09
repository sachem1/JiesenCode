using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models.Dto
{
    public class SelectItem
    {
        public string label { get; set; }

        public object value { get; set; }

        public object parentValue { get; set; } = "";
    }
}