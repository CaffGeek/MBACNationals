using MBACNationals.Tournament.Commands;
using System;
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
        public JsonResult Save(string year)
        {
            var request = this.ControllerContext.HttpContext.Request;
            
            var file = request.Files[0];
			var content = new MemoryStream();
			file.InputStream.CopyTo(content);
            
            var id = request.Form["id"];

            var command = new CreateSponsor
                {
                    Year = year,
                    Id = Guid.NewGuid(),
                    Name = request.Form["name"],
                    Website = request.Form["website"],
                    Image = content.ToArray(),
                };

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}
