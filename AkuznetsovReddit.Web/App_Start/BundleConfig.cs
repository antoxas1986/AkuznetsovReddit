using System.Web.Optimization;

namespace AkuznetsovReddit.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/lib").Include(
              "~/Scripts/jquery-{version}.js",
              "~/Scripts/jquery.unobtrusive-ajax.min.*",
              "~/Scripts/jquery.validate.js",
              "~/Scripts/jquery.validate.unobtrusive.min.js",
              "~/Scripts/modernizr-*",
              "~/Scripts/bootstrap.js",
              "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/site.css"));
        }
    }
}
