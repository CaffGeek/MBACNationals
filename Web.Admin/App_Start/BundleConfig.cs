using System.Web;
using System.Web.Optimization;

namespace WebFrontend
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.jeditable.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/Custom/*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.*",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/ui-bootstrap-tpls-0.9.0.js",
					    "~/Scripts/ng-file-upload-shim.min.js",
                        "~/Scripts/ng-file-upload.min.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/angular-moment.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/AdminApp/Filters/filter.*",
                        "~/AdminApp/app.js",
                        "~/AdminApp/Controllers/controller.*",
                        "~/AdminApp/Directives/directive.*",
                        "~/AdminApp/Factories/factory.*",
                        "~/AdminApp/Services/service.*"));
        }
    }
}