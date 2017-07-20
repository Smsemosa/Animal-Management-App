using System.Web;
using System.Web.Optimization;

namespace ShalomSystem
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include
                         (
                        "~/Scripts/jquery-1.7.1*",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery-unobtrusive-ajax*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery-validate-unobtrusive*"
                        ));


            //     <script src="~/Scripts/jquery-1.9.1.js"></script>
            //<script src="~/Scripts/jquery-2.2.4.min.js"></script>
            //<script src="~/Scripts/jquery.validate.js"></script>
            //<script src="~/Scripts/jquery.unobtrusive-ajax.js"></script>
            //<script src="~/Scripts/jquery.validate.unobtrusive.js"></script>
            //ajax abrousive library 

            bundles.Add(new ScriptBundle("~/bundles/jqueryAjax").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery-unobtrusive-ajax*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery-validate-unobtrusive*"));

            //bundling bootstrap
            //all boostrap scripts
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/Bootstrap/bootstrap.js", "~/Scripts/Bootstrap/bootstrap-min.js"));
            //bootstrap style
            bundles.Add(new StyleBundle("~/BootstrapContent/css").Include("~/Scripts/Bootstrap/bootstrap.css"));



           //bundle toaster notification api
            bundles.Add(new ScriptBundle("~/bundles/Toaster/JS").Include("~/Scripts/ToasterNotification/toastr.js")); 
            bundles.Add(new ScriptBundle("~/bundles/Toaster").Include("~/Scripts/ToasterNotification/jquery-migrate-*"));
            bundles.Add(new StyleBundle("~/ToasterContent/css").Include("~/Scripts/ToasterNotification/all.min.css", "~/Scripts/ToasterNotification/toastr.css", "~/Scripts/ToasterNotification/toastr.min.css"));
           


            //Morris Api 
            bundles.Add(new ScriptBundle("~/bundles/MorrisApi/Js").Include("~/Scripts/MorrisApiGraphs/jquery.*", "~/Scripts/MorrisApiGraphs/morris-min.js", "~/Scripts/MorrisApiGraphs/raphael-min.js"));
            bundles.Add(new StyleBundle("~/MorrisApiCss/Css").Include("~/Scripts/MorrisApiGraphs/morris.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
           
            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}