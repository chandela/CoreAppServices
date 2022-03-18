using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CoreAppServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
          

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }

                
            );

            

config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings();
            EnableCorsAttribute enableCorsAttribute = new EnableCorsAttribute("http://localhost:4200","*","*");

            //new EnableCorsAttribute("*",
            //                                   "Origin, Content-Type, Accept",
            //                                   "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(enableCorsAttribute);
        }
    }
}
