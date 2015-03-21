using MBACNationals.Tournament.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class TournamentController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult All()
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var tournaments = Domain.TournamentQueries.GetTournaments();

            return Json(tournaments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Create(CreateTournament command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }
    }
}
