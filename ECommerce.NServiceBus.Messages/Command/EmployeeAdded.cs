using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.NServiceBus.Messages.Command
{
    public class OrderCreated:BaseCommand
    {
        public int OrderId { get; set; }
    }
}
