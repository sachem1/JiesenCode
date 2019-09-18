using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jiesen.WebApi2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
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

        [Route("api/getUserList")]
        public IHttpActionResult GetUserList()
        {
            return Json(UserList);
        }

        [Route("api/login")]
        public IHttpActionResult Login(LoginParam loginParam)
        {
            if (loginParam.UserName == "jiesen" && loginParam.Password == "123456")
            {
                var result = new ReturnResult()
                { Code = 0, Data = new { Token = Guid.NewGuid().ToString(), Expire = 30 }, Message = "登录成功" };
                return Json(result);
            }

            return Json(new ReturnResult() { Code = 1, Message = "登录失败!" });

        }

        public static List<User> UserList = new List<User>();

        [Route("api/getUserInfo")]
        public IHttpActionResult GetUserInfo()
        {
            return Json(new User() { UserName = "123" });
        }

        [Route("api/create")]
        public IHttpActionResult Create(User user)
        {
            UserList.Add(user);
            return Json(0);
        }

        [Route("api/delete")]
        public IHttpActionResult Delete(User user)
        {
            var userInfo = UserList.Find(x => x.UserName == user.UserName);
            if (userInfo != null)
            {
                UserList.Remove(userInfo);
                return Json(0);
            }
            return Json(1);
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

    public class LoginParam
    {
        public string UserName { get; set; }

        public string Password { get; set; }
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
