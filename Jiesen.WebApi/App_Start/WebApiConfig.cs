﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Jiesen.WebApi.Filters;

namespace Jiesen.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ResultFilterAttribute());
        }
    }
}
