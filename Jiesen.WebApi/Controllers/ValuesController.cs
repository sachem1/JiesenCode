using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jiesen.WebApi.Models;

namespace Jiesen.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get1()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("api/getModel")]
        public Test GetModel()
        {
            Test test=new Test();
            test.Id = 12345678913112247;
            test.Name = "zhangsan";
            test.CreateTime=DateTime.Now;
            throw new Exception("错误");
            return test;
        }
    }
}
