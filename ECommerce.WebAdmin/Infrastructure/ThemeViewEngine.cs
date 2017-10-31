using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.WebAdmin.Infrastructure
{
    public class ThemeViewEngine : RazorViewEngine
    {
        public ThemeViewEngine(string themeName)
        {
            ViewLocationFormats = new[]
            {
                "~/Views/Themes/" + themeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + themeName + "/Shared/{0}.cshtml",
            };

            PartialViewLocationFormats = new[]
           {
                "~/Views/Themes/" + themeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + themeName + "/Shared/{0}.cshtml",
            };

            AreaViewLocationFormats = new[]
           {
                "~/Areas/{2}/Views/Themes/" + themeName + "/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Themes/" + themeName + "/Shared/{0}.cshtml",
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/Themes/" + themeName + "/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Themes/" + themeName + "/Shared/{0}.cshtml",
            };
        }
    }
}