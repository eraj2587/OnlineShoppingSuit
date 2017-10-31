using System.Web.Mvc;
using ECommerce.WebAdmin.Infrastructure;

namespace ECommerce.WebAdmin.Controllers
{
    [RequireHttps]
    [LogFilter]
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}