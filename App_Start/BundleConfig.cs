using System.Web;
using System.Web.Optimization;

namespace ShriVivah
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                      "~/Content/Main/bootstrap.min.css"//,
                      //"~/Content/fonts/fonts/Shiv01.ttf",
                      //"~/Content/fonst/fonts/MANGAL.ttf"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/FrontUI/bootstrap.css",
                      "~/Content/FrontUI/fonts/Shiv01.ttf",
                      "~/Content/FrontUI/fonts/MANGAL.ttf",
                      "~/Content/FrontUI/menu.css",
                      "~/Content/FrontUI/style.css",
                      "~/Content/FrontUI/font-awesome.min.css",
                      "~/Content/FrontUI/popuo-box.css"                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                "~/Scripts/Main/jquery.min.js",
                "~/Scripts/Main/bootstrap.min.js",
                "~/Scripts/Main/angular.min.js",
                "~/Scripts/Main/App.js"                
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/FrontUI/jquery.min.js",
                        "~/Scripts/FrontUI/bootstrap.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquerymenus").Include(
                    "~/Scripts/FrontUI/megamenu.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquerymodel").Include(
                    "~/Scripts/FrontUI/jquery.leanModal.min.js",
                    "~/Scripts/FrontUI/easyResponsiveTabs.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryother").Include(
                    "~/Scripts/FrontUI/move-top.js",
                    "~/Scripts/FrontUI/easing.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryindex").Include(
                    "~/Scripts/FrontUI/jquery.magnific-popup.js",
                    "~/Scripts/HelperJs/IndexHelper.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/UserProfile").Include(
                   "~/Scripts/HelperJs/UserProfileHelper.js",
                   "~/Scripts/HelperJs/ValidationHelper.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/EditUserProfile").Include(
                   "~/Scripts/HelperJs/UserEditHelper.js",
                   "~/Scripts/HelperJs/ValidationHelper.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                   "~/Scripts/jquery.signalR-2.2.0.min.js",
                   //"~/SignalR/hubs"
                   "~/Scripts/timer.js"
               ));

            //bundles.Add(new ScriptBundle("~/bundles/HelperJs/gmaps").Include("~/Scripts/HelperJs/gmaps.js"));

            bundles.Add(new ScriptBundle("~/bundles/HelperJs/VendorHelper").Include("~/Scripts/HelperJs/VendorHelper.js"));

            bundles.Add(new StyleBundle("~/Content/Admincss").Include(
                      "~/Content/AdminPanel/bootstrap.min.css",
                      "~/Content/AdminPanel/style.css",
                      "~/Content/AdminPanel/lines.css",
                      "~/Content/AdminPanel/font-awesome.css",
                      "~/Content/AdminPanel/custom.css"                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Adminjqueryindex").Include(
                    "~/Scripts/AdminPanel/jquery.min.js",
                    "~/Scripts/AdminPanel/metisMenu.min.js",
                    "~/Scripts/AdminPanel/custom.js",
                    "~/Scripts/Main/angular.min.js",
                    "~/Scripts/Main/App.js",
                    "~/Scripts/HelperJs/common.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Scripts/HelperJs/common.js",
                    "~/Scripts/HelperJs/spin.js",
                    //"~/Scripts/HelperJs/fontSettings.js",
                    //"~/Scripts/HelperJs/FontHelper.js",
                    "~/Scripts/HelperJs/ShowCustomAlert.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main/UserProfileController").Include("~/Scripts/Main/UserProfileController.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main/Married").Include("~/Scripts/Main/MarriedController.js"));
        }
    }
}
