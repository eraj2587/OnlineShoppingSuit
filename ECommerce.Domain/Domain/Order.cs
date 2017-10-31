using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain
{
    public class Order : ModelBase
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SystemName { get; set; }
        public IEnumerable<Product> Products  { get; set; }
        public CustomerDetail CustomerDetail { get; set; }
    }
}
