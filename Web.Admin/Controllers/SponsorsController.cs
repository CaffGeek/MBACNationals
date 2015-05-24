using System.IO;
using System.Net.Http;
using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    [Authorize(Roles = "Admin, Host")]
    public class SponsorsController : Controller
    {
        public ActionResult List(string year)
        {
            ViewBag.Year = year;
            return View();
        }

        [System.Web.Http.HttpPost]
        public void Save(string year)
        {
            var request = this.ControllerContext.HttpContext.Request;
            var file = request.Files[0];
            var id = request.Form["id"];
            var name = request.Form["name"];
            var website = request.Form["website"];

            //TODO:
        }
    }
}
