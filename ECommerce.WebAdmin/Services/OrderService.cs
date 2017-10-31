using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.WebAdmin.Models;

namespace ECommerce.WebAdmin.Services
{
    public class OrderService :IOrderService
    {
        public List<ProductModel> GetProductModels()
        {
            return new List<ProductModel>()
            {
                new ProductModel() { ProductCode = "PP12345", ProductId = 1,ProductName = "IPhone SX", Quantity = 1},
                new ProductModel() { ProductCode = "PP98765", ProductId = 2,ProductName = "Samsung Galaxy", Quantity = 2},
            };
        }

        public List<OrderViewModel> GetOrderViewModels()
        {
            return new List<OrderViewModel>
            {
                new OrderViewModel() {OrderId = "123", Desc = "Samsung 356", ProductQty = 1},
                new OrderViewModel() {OrderId = "789", Desc = "Iphone 6", ProductQty = 2},
                new OrderViewModel() {OrderId = "123", Desc = "Micromax", ProductQty = 5},
            };
        } 

        public CustomerModel GetCustomerDetails()
        {
            return new CustomerModel
            {
                CustomerId = 1,
                Address = "Pune",
                Mobile = "9876543214",
                Name = "Raj Shelar"
            };

        }

        public void SaveOrderDetails(OrderViewModel orderViewModel) { }
    }

    public interface IOrderService
    {
        List<ProductModel> GetProductModels();
        CustomerModel GetCustomerDetails();
        void SaveOrderDetails(OrderViewModel orderViewModel);
        List<OrderViewModel> GetOrderViewModels();
    }

}