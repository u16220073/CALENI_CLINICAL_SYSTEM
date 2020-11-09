using System;
using System.IO;
using System.Web.Mvc;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;

namespace Hospital_Management_System.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComplaintHelp()
        {
            return View();
        }

        public ActionResult ViewHelpDoc()
        {
            try
            {
                var path = Server.MapPath("~/Content/help/HELP_DOCUMENT.pdf");
                var AccesFilePath = Server.MapPath("~/Content/uploads/HELP_DOCUMENT.pdf");
                using (Viewer viewerObject = new Viewer(path))
                {
                    PdfViewOptions options = new PdfViewOptions(AccesFilePath);
                    viewerObject.View(options);
                }

                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                var fsResult = new FileStreamResult(fileStream, "application/pdf");
                return fsResult;
            }catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }

            //if we got here something went wrong
            return View();
        }

    }
}