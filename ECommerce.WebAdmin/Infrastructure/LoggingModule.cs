using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Core;
using Ninject.Modules;

namespace ECommerce.WebAdmin.Infrastructure
{
    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(x => LogManager.GetLogger(""));
        }
    }
}