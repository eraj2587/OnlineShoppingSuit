using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.NServiceBus.Messages.Command
{
    public abstract class BaseCommand
    {
        public Guid Id { get; set; }

        internal BaseCommand()
        {
            if (Id == Guid.Empty) Id = Guid.NewGuid();
        }
    }
}
