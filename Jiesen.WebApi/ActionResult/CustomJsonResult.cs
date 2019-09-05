using System;
using System.Web;
using System.Web.Mvc;
using Jiesen.Framework.Serializers.JsonConverter;
using Newtonsoft.Json;

namespace Jiesen.WebApi.ActionResult
{
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentException("context not null");

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException();

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data, 
                    new LongToStringConverter(), 
                    new NullableLongToStringConverter(), 
                    new DateTimeToStringConverter(), 
                    new NullableDateTimeToStringConverter()));
            }
        }
    }
}