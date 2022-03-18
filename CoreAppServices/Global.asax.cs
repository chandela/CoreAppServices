using AutoMapper;
using CoreApp.Business.Configuration;
using CoreApp.SharedAssets;
using CoreAppServices.Models;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CoreAppServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {

            ServicePointManager.ServerCertificateValidationCallback
               = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
               {
                   return true;
               };

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            DependencyResolver.SetResolver(new UnityDependencyResolver(ApplicationInitializer.Initialize(this.IntializeUxMapper)));
            AreaRegistration.RegisterAllAreas();
            BundleTable.EnableOptimizations = false;
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

         public void IntializeUxMapper(IMapperConfigurationExpression intializeUxMapper)
        {
            intializeUxMapper.CreateMap<UserSession, JavascriptUserSession>();
        }
    }
}
