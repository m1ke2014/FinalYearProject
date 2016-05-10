using System.Web.Mvc;

namespace FinalYearProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Staff Area button
        [Authorize]
        public ActionResult StaffArea()
        {
            return View("StaffArea");
        }

        // Customer Area button
        public ActionResult CustomerArea()
        {
            return RedirectToAction("../DeclarationOfConformities/CustomerHome");
        }

        public ActionResult Drawings()
        {
            return View("Drawings");
        }

        public ActionResult Procedures()
        {
            return View("Procedures");
        }

    }
}