using System.Web;
using System.Web.Mvc;
using Jiesen.WebApi.ActionResult;
using Jiesen.WebApi.Filters;

namespace Jiesen.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
        }
    }
}
