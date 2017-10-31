using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace ECommerce.WebAdmin.Infrastructure
{
    public class CustomException : FilterAttribute, IExceptionFilter
    {
        private ILog _log;
        public void  OnException(ExceptionContext filterContext)
        {
            _log = DependencyResolver.Current.GetService<ILog>();
            filterContext.ExceptionHandled = true; // exception handled here, no need to capture in application_error event
            _log.Error("Result executed for action : " + filterContext.RouteData.Values["Action"] + " and controller : " + filterContext.RouteData.Values["Controller"]);
            _log.Error("Error occured : "+ filterContext.Exception.Message);

        }
    }
}