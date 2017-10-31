using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NServiceBus;

namespace ECommerce.Web.Infrastructure
{
    public static class NSBExtentions
    {
        public static ConventionsBuilder ModuleConventions(this ConventionsBuilder builder)
        {
            return builder
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("ECommerce") && t.Namespace.Contains("Messages.Command"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("ECommerce") && t.Namespace.Contains("Messages.Event"))
                .DefiningEncryptedPropertiesAs(p => p.Name.EndsWith("Encrypted"))
                .DefiningDataBusPropertiesAs(p => p.Name.EndsWith("DataBus"))
                .DefiningExpressMessagesAs(t => t.Name.EndsWith("Express"));
        }
    }
}