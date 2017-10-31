using System.Collections.Generic;
using System.Web.Mvc;
using ECommerce.WebAdmin.Infrastructure;
using ECommerce.WebAdmin.Models;
using ECommerce.WebAdmin.Services;

namespace ECommerce.WebAdmin.Controllers
{
    public class OrderController : BaseController
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Order
        public ActionResult PlaceOrder()
        {
            OrderViewModel model = new OrderViewModel();
            model.ProductModels = _orderService.GetProductModels().ToArray();
            return View(model);
        }

        // GET: Order
        public ActionResult ViewOrders()
        {
            return View(_orderService.GetOrderViewModels());
        }

        [HttpPost]
        public ActionResult ExportToCsv(List<OrderViewModel> model)
        {
            return new ExportResult(_orderService.GetOrderViewModels());
        }

        // GET: Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View(orderViewModel);
        }
    }
}