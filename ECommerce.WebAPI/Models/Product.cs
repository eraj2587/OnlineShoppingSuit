using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.WebAPI.Models
{
    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Product Name is required", AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "Min Lenght is 6 chars")]
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required",AllowEmptyStrings = false)]
        [MinLength(5,ErrorMessage = "Min Lenght is 5 chars")]
        [MaxLength(11, ErrorMessage = "Max Lenght is 11 chars")]
        public string ProductName { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}