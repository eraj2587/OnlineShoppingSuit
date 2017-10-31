using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace ECommerce.WebAdmin.Infrastructure
{
    public class LogFilter : ActionFilterAttribute
    {
        private ILog _log;

        public LogFilter()
        {
            _log= DependencyResolver.Current.GetService<ILog>();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _log.Info("Action executed for action : "+ filterContext.ActionDescriptor.ActionName+" and controller : "+filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _log.Info("Result executed for action : " + filterContext.RouteData.Values["Action"] + " and controller : " + filterContext.RouteData.Values["Controller"]);

            base.OnResultExecuted(filterContext);
        }
    }
}