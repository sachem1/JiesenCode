using System.Web.Mvc;

namespace Epass.Vue.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "加贸云Vue Api";

            return View();
        }
    }
}
