using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ECommerce.Common;
using ECommerce.Storage.Repository;
using log4net;

namespace ECommerce.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            if (exception == null)
                return;
           // IocContainer.Instance.Get<ILog>().Error("Unhandled exception:" + exception.StackTrace, exception);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            IocContainer.Instance.Get<IUnitOfWork>().CommitChanges();
        }
    }
}
