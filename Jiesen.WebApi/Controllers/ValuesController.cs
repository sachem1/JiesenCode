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
        public IHttpActionResult Logout(LoginOutParam param)
        {
            if (string.IsNullOrEmpty(param.UserId)) return Json(1);
            var res = new List<SelectItem>() { new SelectItem() { label = "测试", value = 1 } };

            return Json(res);
        }


        [Route("api/auth/login")]
        [HttpPost]
        public IHttpActionResult Login(LoginParam param)
        {
            if (string.IsNullOrEmpty(param.UserName) && string.IsNullOrEmpty(param.Password)) return Json(1);
            ReturnResult<BaseModel> r = new ReturnResult<BaseModel>();
            r.Result = new
            {
                Token = "fajfljdsafsdf456s41f6ds",
                UserId = 2,
                LoginName = param.UserName,
                param.UserName,
                LogonTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return Json(r);
        }
    }
}
