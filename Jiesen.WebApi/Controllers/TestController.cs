using Jiesen.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Jiesen.WebApi.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }

        [Route("api/userService/getTestList")]
        public List<Test> GetTestList()
        {
            return new List<Test>()
            {
                new Test(){Name="1123",Address="ddddd",Age=23},
                new Test(){Name="3333",Address="yherty",Age=21},
                new Test(){Name="4444",Address="fdsafa",Age=20}
            };
        }



        [Route("api/tradeService/CreateData")]
        public IHttpActionResult CreateData(Test test)
        {
            return Json(0);
        }

        [Route("api/tradeService/exportPdf")]
        public HttpResponseMessage exportPdf()
        {
            var fileFulllName = AppDomain.CurrentDomain.BaseDirectory + "download/test.pdf";
            if (File.Exists(fileFulllName))
            {
                var downFileName = new FileInfo(fileFulllName).Name;
                var dataBytes = File.ReadAllBytes(fileFulllName);
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                httpResponseMessage.Content = new ByteArrayContent(dataBytes);
                httpResponseMessage.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment");
                httpResponseMessage.Content.Headers.ContentDisposition.FileName = downFileName;
                httpResponseMessage.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
                httpResponseMessage.Headers.Add("Access-Control-Expose-Headers", "FileName");
                httpResponseMessage.Headers.Add("FileName", HttpUtility.UrlEncode(downFileName));
                return httpResponseMessage;
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }



    }
}
