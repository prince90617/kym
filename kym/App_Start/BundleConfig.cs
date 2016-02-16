using System.Web;
using System.Web.Optimization;

namespace kym
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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
            bundles.Add(new StyleBundle("~/Scripts/commonscripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/app/header.js",
                        "~/Scripts/jquery.colorbox.js",
                        "~/Scripts/bootstrap.min.js"
                        ));
            bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                       "~/Content/bootstrap.min.css",
                       "~/Content/site.css",
                       "~/Content/colorbox/colorbox.css"
                       ));
            bundles.Add(new StyleBundle("~/Vendor/Indexcss").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/themes/Vendor/morris/morris.css",
                      "~/Content/themes/Vendor/jvectormap/jquery-jvectormap-1.2.2.css",
                      "~/Content/themes/Vendor/datepicker/datepicker3.css",
                      "~/Content/themes/Vendor/daterangepicker/daterangepicker-bs3.css",
                      "~/Content/themes/Vendor/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css",
                      "~/Content/themes/Vendor/AdminLTE.css"
                      ));

            bundles.Add(new StyleBundle("~/Scripts/Vendor/Index").Include(
                //morris chart
                     "~/Scripts/raphael-min.js",
                      "~/Scripts/Vendor/plugins/morris/morris.min.js",
                //Sparkline
                       "~/Scripts/Vendor/plugins/sparkline/jquery.sparkline.min.js",
                //jvectormap
                // "~/Scripts/Vendor/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                //"~/Scripts/Vendor/plugins/jvectormap/jquery-jvectormap-world-mill-en.js",
                //jQuery Knob Chart
                       "~/Scripts/Vendor/plugins/jqueryKnob/jquery.knob.js",
                //daterangepicker
                       "~/Scripts/Vendor/plugins/daterangepicker/daterangepicker.js",
                //datepicker
                       "~/Scripts/Vendor/plugins/datepicker/bootstrap-datepicker.js",
                //Bootstrap WYSIHTML5
                       "~/Scripts/Vendor/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js",
                //iCheck
                       "~/Scripts/Vendor/plugins/iCheck/icheck.min.js",
                //AdminLTE App
                       "~/Scripts/Vendor/AdminLTE/app.js",
                //AdminLTE for demo purposes
                       "~/Scripts/Vendor/AdminLTE/demo.js",
                //AdminLTE dashboard demo (This is only for demo purposes)
                       "~/Scripts/Vendor/AdminLTE/dashboard.js"

                     ));
        }
    }
}