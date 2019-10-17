using Jiesen.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace Jiesen.WebApi.Extensions
{
    public static class StartupExtensions
    {
        public static void Formatters(this HttpConfiguration configuration)
        {
            configuration.Formatters.JsonFormatter.SerializerSettings=new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new NullableLongToStringConverter());
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new LongToStringConverter());
            configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new DateTimeToStringConverter());
        }

    }
}