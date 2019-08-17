using System.Web;
using System.Web.Optimization;

namespace UniversityCourseManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/js/vendor/jquery-1.12.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/js/vendor/modernizr-2.8.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/js/wow.min.js",
                      "~/Scripts/js/jquery-price-slider.js",
                      "~/Scripts/js/jquery.meanmenu.js",
                      "~/Scripts/js/owl.carousel.min.js",
                      "~/Scripts/js/jquery.sticky.js",
                      "~/Scripts/js/jquery.scrollUp.min.js",
                      "~/Scripts/js/counterup/jquery.counterup.min.js",
                      "~/Scripts/js/counterup/waypoints.min.js",
                      "~/Scripts/js/counterup/counterup-active.js",
                      "~/Scripts/js/scrollbar/jquery.mCustomScrollbar.concat.min.js",
                      "~/Scripts/js/scrollbar/mCustomScrollbar-active.js",
                      "~/Scripts/js/metisMenu/metisMenu.min.js",
                      "~/Scripts/js/metisMenu/metisMenu-active.js",
                      "~/Scripts/js/morrisjs/raphael-min.js",
                      "~/Scripts/js/morrisjs/morris.js",
                      "~/Scripts/js/morrisjs/morris-active.js",
                      "~/Scripts/js/sparkline/jquery.sparkline.min.js",
                      "~/Scripts/js/sparkline/jquery.charts-sparkline.js",
                      "~/Scripts/js/sparkline/sparkline-active.js",
                      "~/Scripts/js/calendar/moment.min.js",
                      "~/Scripts/js/calendar/fullcalendar.min.js",
                      "~/Scripts/js/calendar/fullcalendar-active.js",
                      "~/Scripts/js/plugins.js",
                      "~/Scripts/js/main.js",
                      "~/Scripts/js/tawk-chat.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/owl.carousel.css",
                      "~/Content/css/owl.theme.css",
                      "~/Content/css/owl.transitions.css",
                      "~/Content/css/animate.css",
                      "~/Content/css/normalize.css",
                      "~/Content/css/meanmenu.min.css",
                      "~/Content/css/main.css",
                      "~/Content/css/educate-custon-icon.css",
                      "~/Content/css/morrisjs/morris.css",
                      "~/Content/css/scrollbar/jquery.mCustomScrollbar.min.css",
                      "~/Content/css/metisMenu/metisMenu.min.css",
                      "~/Content/css/metisMenu/metisMenu-vertical.css",
                      "~/Content/css/calendar/fullcalendar.min.css",
                      "~/Content/css/calendar/fullcalendar.print.min.css",
                      "~/Content/style.css",
                      "~/Content/css/responsive.css"));
        }
    }
}
