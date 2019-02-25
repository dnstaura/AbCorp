using System.Web;
using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace testuser
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Bundles/css")
    .Include("~/Content/css/bootstrap.min.css", new CssRewriteUrlTransformAbsolute())
    .Include("~/Content/css/bootstrap-select.css")
    .Include("~/Content/css/bootstrap-clockpicker.css")
    .Include("~/Content/css/bootstrap-datepicker3.min.css")
    .Include("~/Content/css/iziModal.min.css")
    .Include("~/Content/datatables.min.css")
    .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
    .Include("~/Content/css/icheck/blue.min.css", new CssRewriteUrlTransformAbsolute())
    .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransformAbsolute())
    .Include("~/Content/css/skins/_all-skins.min.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/fullcalendar/moment.min.js")
                .Include("~/Content/js/plugins/fullcalendar/fullcalendar.min.js")
                .Include("~/Content/js/plugins/fullcalendar/es.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
                .Include("~/Content/js/plugins/Chart/Chart.bundle.js")
                .Include("~/Content/js/plugins/ClockPicker/bootstrap-clockpicker.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/pickadate/picker.js")
                .Include("~/Content/js/plugins/pickadate/picker.date.js")
                .Include("~/Content/js/plugins/pickadate/picker.time.js")
                .Include("~/Content/js/plugins/pickadate/legacy.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/plugins/iziModal/iziModal.min.js")
                .Include("~/Content/js/adminlte.js")
                .Include("~/Content/datatables.min.js")
                .Include("~/Content/js/init.js"));



            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            //// para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Bundles/css")
            //    .Include("~/Content/css/bootstrap.min.css")
            //    .Include("~/Content/css/bootstrap-select.css")
            //    .Include("~/Content/css/bootstrap-datepicker3.min.css")
            //    .Include("~/Content/css/font-awesome.min.css")
            //    .Include("~/Content/css/icheck/blue.min.css")
            //    .Include("~/Content/css/AdminLTE.css")
            //    .Include("~/Content/css/skins/_all-skins.css"));

            //bundles.Add(new ScriptBundle("~/Bundles/js")
            //    .Include("~/Content/js/plugins/jquery/jquery-3.3.1.js")
            //    .Include("~/Content/js/plugins/bootstrap/bootstrap.js")
            //    .Include("~/Content/js/plugins/fastclick/fastclick.js")
            //    .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
            //    .Include("~/Content/js/plugins/bootstrap-select/bootstrap-select.js")
            //    .Include("~/Content/js/plugins/moment/moment.js")
            //    .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
            //    .Include("~/Content/js/plugins/icheck/icheck.js")
            //    .Include("~/Content/js/plugins/validator.js")
            //    .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
            //    .Include("~/Content/js/adminlte.js")
            //    .Include("~/Content/js/init.js"));
        }
    }
}
