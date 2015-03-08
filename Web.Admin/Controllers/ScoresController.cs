using MBACNationals.Scores.Commands;
using System;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class ScoresController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Entry");
        }

        [Authorize(Roles = "ScoreEntry, Admin")]
        public ActionResult Entry()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Schedule(string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var schedule = Domain.ScheduleQueries.GetSchedule(division);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Standings(string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var standings = Domain.StandingQueries.GetDivision(division);
            return Json(standings, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Match(Guid matchId)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var match = Domain.MatchQueries.GetMatch(matchId);
            return Json(match, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Participant(Guid participantId)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var participant = Domain.ParticipantScoreQueries.GetParticipant(participantId);
            return Json(participant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Team(Guid teamId)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var team = Domain.TeamScoreQueries.GetTeam(teamId);
            return Json(team, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult HighScores(string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var hightScores = Domain.HighScoreQueries.GetDivision(division);
            return Json(hightScores, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "ScoreEntry, Admin")]
        public JsonResult SaveMatchResult(SaveMatchResult command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}
