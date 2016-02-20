using MBACNationals.Scores.Commands;
using System;
using System.Linq;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    public class ScoresController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Entry");
        }

        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public ActionResult Entry()
        {
            return View();
        }

        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public ActionResult Stepladder(string year)
        {
            ViewBag.Year = year;
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Schedule(int year, string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var schedule = Domain.ScheduleQueries.GetSchedule(year, division);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Standings(string year, string division)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var tournament = Domain.TournamentQueries.GetTournament(year);
            var tournamentId = (year == "2014") ? Guid.Empty : tournament.Id;
                
            var standings = Domain.StandingQueries.GetDivision(tournamentId, division);
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
        public JsonResult HighScores(string division, int year)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var hightScores = Domain.HighScoreQueries.GetDivision(division, year);
            return Json(hightScores, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult StepladderMatches(string year)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var matches = Domain.StepladderQueries.GetMatches(year);
            return Json(matches, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public JsonResult SaveMatchResult(SaveMatchResult command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public JsonResult CreateStepladderMatch(CreateStepladderMatch command)
        {
            command.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(command);
            var match = Domain.StepladderQueries.GetMatches(command.Year)
                .FirstOrDefault(x => x.Id == command.Id);
            return Json(match);
        }

        [HttpPost]
        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public JsonResult UpdateStepladderMatch(UpdateStepladderMatch command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [Authorize(Roles = "ScoreEntry, Host, Admin")]
        public JsonResult DeleteStepladderMatch(DeleteStepladderMatch command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}
