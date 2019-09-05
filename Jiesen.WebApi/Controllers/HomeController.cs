
using System.Web.Mvc;

namespace Jiesen.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public System.Web.Mvc.ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
