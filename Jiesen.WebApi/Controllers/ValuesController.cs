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
            Test test = new Test();
            test.Id = 12345678913112247;
            test.Name = "zhangsan";
            test.CreateTime = DateTime.Now;
            throw new Exception("错误");
            return test;
        }


        [Route("api/getUserList")]
        public IHttpActionResult GetUserList()
        {
            return Json(GetInitData());
        }
        [Route("api/login")]
        public IHttpActionResult Login(string userName, string password)
        {
            if (userName == "jiesen" && password == "123456")
            {
                var result = new ReturnResult()
                { Code = 0, Data = new { Token = Guid.NewGuid().ToString(), Expire = 30 }, Message = "登录成功" };
                return Json(result);
            }

            return Json(new ReturnResult() { Code = 1, Message = "登录失败!" });

        }

        private List<User> GetInitData()
        {
            List<User> users = new List<User>()
            {
                new User(){UserName = ""}
            };
            return users;
        }

    }

    public class ReturnResult
    {
        public int Code { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }

}
