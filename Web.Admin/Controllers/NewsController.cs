using MBACNationals.Tournament.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class NewsController : Controller
    {
        [Authorize(Roles = "Admin, Host")]
        public ActionResult Edit(string year)
        {
            ViewBag.Year = year;
            return View();
        }

        [System.Web.Http.HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult List(string year)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            var news = Domain.TournamentQueries.GetNews(year);
            return Json(news, JsonRequestBehavior.AllowGet);
        }
        
        [System.Web.Http.HttpPost]
        public JsonResult Save(CreateNews command)
        {
            command.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public void Delete(string year, string id)
        {
            var command = new DeleteNews
            {
                Id = Guid.Parse(id),
                Year = year
            };

            Domain.Dispatcher.SendCommand(command);
        }
    }
}
