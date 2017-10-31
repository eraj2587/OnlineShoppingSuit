using ECommerce.WebAdmin.Model;
using ECommerce.WebAdmin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.WebAdmin.Infrastructure;

namespace ECommerce.WebAdmin.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IMailService _mail;

        public HomeController(IMailService mail)
        {
            _mail = mail;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Single()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        [HttpPost]
        [Authorize]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website: {3}{0}Comment:{4}",
            Environment.NewLine,
            model.name,
            model.email,
            model.website,
            model.comment);

            if (_mail.SendEmail("noreply@yourdomain.com",
              "foo@yourdomain.com",
              "Website Contact",
              msg))
            {
                ViewBag.MailSent = true;
            }

            return View();
        }
    }
}