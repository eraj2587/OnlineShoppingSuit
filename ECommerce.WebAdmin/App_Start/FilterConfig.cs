using System.Web;
using System.Web.Mvc;
using ECommerce.WebAdmin.Infrastructure;

namespace ECommerce.WebAdmin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomException());
        }
    }
}
