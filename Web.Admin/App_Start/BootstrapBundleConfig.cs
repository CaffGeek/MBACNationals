using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(WebFrontend.App_Start.BootstrapBundleConfig), "RegisterBundles")]

namespace WebFrontend.App_Start
{
	public class BootstrapBundleConfig
	{
		public static void RegisterBundles()
		{
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap/base").Include("~/Content/bootstrap/bootstrap.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap/theme").Include("~/Content/bootstrap/themes/justified-nav.css"));
		}
	}
}
