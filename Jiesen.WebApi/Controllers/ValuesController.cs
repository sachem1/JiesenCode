using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Epass.Vue.WebApi.Models;
using Epass.Vue.WebApi.Models.Dto;
using Newtonsoft.Json;

namespace Epass.Vue.WebApi.Controllers
{
    /// <summary>
    /// 前端获取json涉及打包问题,暂时以这种方式
    /// </summary>
    public class ValuesController : ApiController
    {
        [Route("api/getMetadata")]

        public IHttpActionResult GetMetaData(string name)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"metadata/{name}.json");
            if (File.Exists(filePath))
            {
                var json = JsonConvert.DeserializeObject(File.ReadAllText(filePath, System.Text.Encoding.UTF8));
                return Json(json);
            }
            else
            {
                return null;
            }
        }

        [Route("api/auth/menus")]

        public IHttpActionResult GetMenus(string loginName)
        {
            loginName = "routerrules";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"metadata/{loginName}.json");
            if (File.Exists(filePath))
            {
                var json = JsonConvert.DeserializeObject(File.ReadAllText(filePath, System.Text.Encoding.UTF8));
                return Json(json);
            }
            else
            {
                return null;
            }
        }


        [Route("api/auth/systems")]
        public IHttpActionResult GetChildrenSystemData(string u)
        {
            if (string.IsNullOrEmpty(u)) return Json(1);
            var res = new List<SelectItem>() { new SelectItem() { label = "测试", value = 1 } };

            return Json(res);
        }

        [Route("api/auth/logout")]
        [HttpPost]
        public ReturnResult<object> Logout(LoginOutParam param)
        {
            if (string.IsNullOrEmpty(param.UserId)) return new ReturnResult<object>() { };
            var res = new List<SelectItem>() { new SelectItem() { label = "测试", value = 1 } };

            return new ReturnResult<object>() { Data = res };
        }


        [Route("api/auth/login")]
        [HttpPost]
        public ReturnResult<object> Login(LoginParam param)
        {
            if (string.IsNullOrEmpty(param.LoginName) && string.IsNullOrEmpty(param.Password)) return new ReturnResult<object> {Status=1 };
            ReturnResult<object> r = new ReturnResult<object>();
            r.Data = new
            {
                Token = "fajfljdsafsdf456s41f6ds",
                UserId = 2,
                LoginName = param.LoginName,
                LogonTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return r;
        }
    }
}
