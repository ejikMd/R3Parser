using System.Web.Optimization;

namespace R3
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/angular")
                            .Include("~/Scripts/libs/angular.js")
                            .Include("~/Scripts/libs/angular-animate.js")
                        );

            bundles.Add(new StyleBundle("~/Content/css")
                            .Include("~/Content/site.css")
                            .Include("~/Content/bootstrap.css")
                        );
        }
    }
}