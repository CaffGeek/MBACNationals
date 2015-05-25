using MBACNationals.Tournament.Commands;
using System;
using System.IO;
using System.Net.Http;
using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    public class SponsorsController : Controller
    {
        [Authorize(Roles = "Admin, Host")]
        public ActionResult Edit(string year)
        {
            ViewBag.Year = year;
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult List(string year)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            var sponsors = Domain.TournamentQueries.GetSponsors(year);
            return Json(sponsors, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public FileResult Image(string id)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            var bytes = Domain.TournamentQueries.GetSponsorImage(Guid.Parse(id));
            return new FileStreamResult(new MemoryStream(bytes), "image");
        }

        [Authorize(Roles = "Admin, Host")]
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


        [Authorize(Roles = "Admin, Host")]
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(string year, string id)
        {
            var command = new DeleteSponsor
            {
                Id = Guid.Parse(id),
                Year = year
            };

            Domain.Dispatcher.SendCommand(command);
        }
    }
}
