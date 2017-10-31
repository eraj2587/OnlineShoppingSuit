using System.Web;
using System.Web.Optimization;

namespace ECommerce.WebAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/commonNg").Include(
                      "~/Scripts/angular.js", "~/Scripts/angular-resource.js",
                      "~/Scripts/angular-route.js",
                      "~/Scripts/angular-cookies.js",
                        "~/Scripts/ng-table.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/commonApp").Include(
                     "~/app/app.js", "~/app/services/common.js",
                     "~/app/services/productService.js",
                     "~/app/services/persistenceService.js",
                     "~/app/services/userAccountService.js"
        ));

            bundles.Add(new ScriptBundle("~/bundles/commonCtrl").Include(
                    "~/app/controllers/mainCtrl.js",
                    "~/app/controllers/productDetailCtrl.js",
                    "~/app/controllers/productEditCtrl.js",
                    "~/app/controllers/productListCtrl.js"
       ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                       "~/Content/app.css",
 "~/Content/ng-table.css"
                      ));
        }
    }
}
