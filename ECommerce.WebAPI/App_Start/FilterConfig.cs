using ECommerce.WebAPI.Helpers;
using System.Web.Mvc;

namespace ECommerce.WebAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new WebApiValidateAntiForgeryTokenAttribute());
        }
    }
}
