using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain;

namespace ECommerce.Domain{
    public class CustomerDetail
    {
        public int CustomerDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public IEnumerable<ShippingDetail> ShippingDetails { get; set; } 
    }
}
