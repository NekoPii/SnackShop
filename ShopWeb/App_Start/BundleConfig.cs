using System.Web;
using System.Web.Optimization;

namespace ShopWeb
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/css/css").Include(
       "~/Content/css/bootstrap-tagsinput.css",
       "~/Content/css/responsive.css",
       "~/Content/css/step-wizard.css",
       "~/Content/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/js/js").Include(
          "~/Content/js/bootstrap-tagsinput.min.js",
          "~/Content/js/custom.js",
          "~/Content/js/jquery.countdown.min.js",
          "~/Content/js/jquery-3.3.1.min.js",
          "~/Content/js/offset_overlay.js"));

            bundles.Add(new StyleBundle("~/Content/vender/css").Include(
          "~/Content/vendor/bootstrap/css/bootstrap.min.css",
          "~/Content/vendor/fontawesome-free/css/all.min.css",
          "~/Content/vendor/OwlCarousel/assets/owl.carousel.css",
          "~/Content/vendor/OwlCarousel/assets/owl.theme.default.min.css",
          "~/Content/vendor/semantic/semantic.min.css",
          "~/Content/vendor/unicons-2.0.1/css/unicons.css"));

            bundles.Add(new StyleBundle("~/Content/vender/js").Include(
        "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js",
        "~/Content/vendor/OwlCarousel/owl.carousel.js",
        "~/Content/vendor/semanticsemantic.min.js"));
        }
    }
}
