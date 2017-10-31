using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Domain;

namespace ECommerce.Web.ViewModel
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SystemName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
    }
  
}