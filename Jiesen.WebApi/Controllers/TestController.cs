using Jiesen.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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



    }
}
