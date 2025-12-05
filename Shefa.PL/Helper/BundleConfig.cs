using System.Web;
using System.Web.Optimization;

namespace SmartHealthCare
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Turn off bundling/minification while debugging
            BundleTable.EnableOptimizations = false;

            // jQuery (You can keep it for validation and other scripts)
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // jQuery validation (optional but useful)
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Popper + Bootstrap (Bootstrap 5 Version!)
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/popper.min.js",
                        "~/Scripts/bootstrap.bundle.js"   // bundle.js already includes Popper
            ));

            // CSS
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"));
        }
    }
}
