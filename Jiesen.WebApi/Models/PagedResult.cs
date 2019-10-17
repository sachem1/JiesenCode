using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Epass.Vue.WebApi.Models
{
    public class PagedResult<T> where T : class
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }

    }
}