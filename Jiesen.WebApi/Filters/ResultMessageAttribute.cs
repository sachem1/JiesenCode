using System.Web.Mvc;
using Jiesen.Core.Enums;
using Jiesen.Core.Models;
using Jiesen.WebApi.ActionResult;

namespace Jiesen.WebApi.Filters
{
    public class ResultMessageAttribute : ActionFilterAttribute
    {
        private object GetResultMessage(object data)
        {
            if (data is BaseMessage)
                return data;
            return new ResultMessage(ResultState.Success, "Success", data);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var jsonResult = filterContext.Result as CustomJsonResult ?? filterContext.Result as JsonResult;
            if (jsonResult == null)
            {
                return;
            }
            jsonResult.Data = GetResultMessage(jsonResult.Data);
            filterContext.Result = jsonResult;
        }
    }
}