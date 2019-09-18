using System;
using System.Web.Http;
using Jiesen.Core.Enums;
using Jiesen.Core.Models;

namespace Jiesen.Framework.WebApi
{
    public class BaseApiController : ApiController
    {
        protected ResultMessage AjaxOkResult(object data = null, string message = "success")
        {
            return new ResultMessage(ResultState.Success, message, data);
        }

        protected ResultMessage AjaxExceptionResult(Exception ex, object data = null)
        {
            return new ResultMessage(ResultState.Fail, ex.ToString(), data);
        }
    }
}
