using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain
{
    public class Product : ModelBase
    {
        public int Id   { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
    }
}
