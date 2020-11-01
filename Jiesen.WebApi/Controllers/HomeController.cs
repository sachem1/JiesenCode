using System;
using System.IO;
using System.Web.Mvc;
using iTextSharp.text.pdf;

namespace Jiesen.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "加贸云Vue Api";

            return View();
        }

        public ActionResult exportPdf()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "download/test.pdf";
            PdfDocument doc = new PdfDocument();
            using (FileStream fileStream =new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                PdfWriter pdfWriter = PdfWriter.GetInstance(doc, fileStream);
              
            }
            
          



            return File(path, "application/pdf", "test.pdf");

        }

    }
}
