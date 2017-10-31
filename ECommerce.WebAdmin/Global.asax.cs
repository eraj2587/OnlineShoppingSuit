using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ECommerce.WebAdmin.Infrastructure;
using WebGrease.Configuration;

namespace ECommerce.WebAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if(ConfigurationManager.AppSettings["activeTheme"] !=null)
            ViewEngines.Engines.Insert(0,new ThemeViewEngine(ConfigurationManager.AppSettings["activeTheme"]));


           

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
