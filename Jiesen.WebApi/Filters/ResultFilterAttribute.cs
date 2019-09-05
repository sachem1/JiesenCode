using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Jiesen.Framework.Enums;
using Jiesen.Framework.Models;

namespace Jiesen.WebApi.Filters
{
    /// <summary>
    /// 统一处理API返回结果
    /// </summary>
    public class ResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if (context.Exception != null)
            {
                return;
            }

            var isActionResult = typeof(IHttpActionResult).IsAssignableFrom(context.ActionContext.ActionDescriptor.ReturnType);
            if (isActionResult)
            {
                return;
            }
            var httpStatusCodes = new List<HttpStatusCode>
            {
                HttpStatusCode.OK,
                HttpStatusCode.NoContent
            };
            if (!httpStatusCodes.Contains(context.Response.StatusCode))
            {
                throw new Exception($"{context.Response.StatusCode}");
            }
            var httpResponseMessage = context.Response.Content as ObjectContent;
            if (httpResponseMessage?.Value is ResultMessage)
            {
                return;
            }
            var apiMessage = new ResultMessage(
                ResultState.Success,
                "success",
                httpResponseMessage?.Value);

            context.Response = context.Request.CreateResponse(apiMessage);
        }
    }
}