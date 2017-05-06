using CaptchaMvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using tbcng.Models;

namespace tbcng
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
            Database.SetInitializer<ApplicationDbContext>(null);
            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());

            // Replace the Default WebFormViewEngine with our custom WebFormThemeViewEngine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemedRazorViewEngine(ConfigurationManager.AppSettings["currentTheme"]));
        }
    }
}
