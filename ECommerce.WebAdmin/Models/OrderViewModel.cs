using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECommerce.WebAdmin.Attributes;
using FileHelpers;

namespace ECommerce.WebAdmin.Models
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines]
    public class OrderViewModel
    {
       
        [Display(AutoGenerateField = false,Name ="Order ID")]
        public string OrderId { get; set; }
        [StringLength(250)]
        [Required]
        [Display(Name = "Description")]
        public string Desc { get; set; }
        [Required]
        [IsNumeric(ErrorMessage = "Please enter numberic value")]
        [Display(Name = "Quantity")]
        public int ProductQty { get; set; }

        [FieldHidden]
        public ProductModel[] ProductModels;

        [FieldHidden]
        public CustomerModel CustomerModel;
    }

    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }

    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
    }


}