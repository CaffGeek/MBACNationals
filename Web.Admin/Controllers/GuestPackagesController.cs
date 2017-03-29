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
    public class GuestPackagesController : Controller
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
            var hotels = Domain.TournamentQueries.GetGuestPackages(year);
            return Json(hotels, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Http.HttpPost]
        public JsonResult Save(SaveGuestPackages command)
        {
            command.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}