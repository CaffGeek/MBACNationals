using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Edit", "Contingent");
        }
    }
}
