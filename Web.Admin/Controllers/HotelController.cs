using MBACNationals.Tournament.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class HotelController : Controller
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
            var hotels = Domain.TournamentQueries.GetHotels(year);
            return Json(hotels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public FileResult Image(string id)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            var bytes = Domain.TournamentQueries.GetHotelImage(Guid.Parse(id));
            return new FileStreamResult(new MemoryStream(bytes), "image");
        }

        [Authorize(Roles = "Admin, Host")]
        [System.Web.Http.HttpPost]
        public JsonResult Save(string year)
        {
            var request = this.ControllerContext.HttpContext.Request;

            var logo = new MemoryStream();
            request.Files[0].InputStream.CopyTo(logo);

            var image = new MemoryStream();
            request.Files[1].InputStream.CopyTo(image);

            var command = new CreateHotel
            {
                Year = year,
                Id = Guid.NewGuid(),
                Name = request.Form["name"],
                Website = request.Form["website"],
                Logo = logo.ToArray(),
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
            var command = new DeleteHotel
            {
                Id = Guid.Parse(id),
                Year = year
            };

            Domain.Dispatcher.SendCommand(command);
        }
    }
}