using MBACNationals.Tournament.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class CentresController : Controller
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
            var centres = Domain.TournamentQueries.GetCentres(year);
            return Json(centres, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public FileResult Image(string id)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            var bytes = Domain.TournamentQueries.GetCentreImage(Guid.Parse(id));
            return new FileStreamResult(new MemoryStream(bytes), "image");
        }

        [Authorize(Roles = "Admin, Host")]
        [System.Web.Http.HttpPost]
        public JsonResult Save(string year)
        {
            var request = this.ControllerContext.HttpContext.Request;

            var image = new MemoryStream();
            request.Files[0].InputStream.CopyTo(image);

            var command = new CreateCentre
            {
                Year = year,
                Id = Guid.NewGuid(),
                Name = request.Form["name"],
                Website = request.Form["website"],
                PhoneNumber = request.Form["phonenumber"],
                Address = request.Form["address"],
                Image = image.ToArray(),
            };

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [Authorize(Roles = "Admin, Host")]
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(string year, string id)
        {
            var command = new DeleteCentre
            {
                Id = Guid.Parse(id),
                Year = year
            };

            Domain.Dispatcher.SendCommand(command);
        }
    }
}