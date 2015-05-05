using MBACNationals.Contingent.Commands;
using MBACNationals.Participant.Commands;
using System;
using System.Linq;
using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{    
    public class ContingentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Reservation(string year, string province)
        {
            return loadView(year, province);
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Edit(string year, string province)
        {
            return loadView(year, province);
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Arrivals(string year, string province)
        {
            return loadView(year, province);
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Practice(string year, string province)
        {
            return loadView(year, province);
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Profiles(string year, string province)
        {
            return loadView(year, province);
        }

        private ActionResult loadView(string year, string province)
        {
            if (string.IsNullOrWhiteSpace(year))
                return RedirectToAction("Index", "Tournament");
            ViewBag.Year = year;

            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");
            ViewBag.Province = province;

            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Index(string year, string province)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            province = province.ToUpper();

            var tournament = Domain.TournamentQueries.GetTournament(year);
            var contingent = Domain.ContingentViewQueries.GetContingent(tournament.Id, province);

            if (contingent != null)
                return Json(contingent, JsonRequestBehavior.AllowGet);
            
            var command = new MBACNationals.Contingent.Commands.CreateContingent();
            command.Id = Guid.NewGuid();
            command.Province = province;
            command.TournamentId = tournament.Id;
            Domain.Dispatcher.SendCommand(command);
            contingent = Domain.ContingentViewQueries.GetContingent(tournament.Id, province);

            return Json(contingent, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Team(string year, string province, string teamName)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var tournament = Domain.TournamentQueries.GetTournament(year);
            var theContingent = Domain.ContingentViewQueries.GetContingent(tournament.Id, province);
            if (theContingent == null)
                return null; //TODO: Return an error???

            return Json(theContingent.Teams.SingleOrDefault(x => x.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase)), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult TravelPlans(string year, string province)
        {
            if (string.IsNullOrWhiteSpace(province))
            {
                var travelPlans = Domain.ContingentTravelPlanQueries.GetAllTravelPlans(year);
                return Json(travelPlans, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var travelPlans = Domain.ContingentTravelPlanQueries.GetTravelPlans(year, province);
                return Json(travelPlans, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Rooms(string year, string province)
        {
            if (string.IsNullOrWhiteSpace(province))
            {
                var rooms = Domain.ContingentTravelPlanQueries.GetAllRooms(year);
                return Json(rooms, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rooms = Domain.ContingentTravelPlanQueries.GetRooms(year, province);
                return Json(rooms, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult PracticePlan(string year, string province)
        {

            if (string.IsNullOrWhiteSpace(province))
            {
                var rooms = Domain.ContingentPracticePlanQueries.GetAllSchedules(year);
                return Json(rooms, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rooms = Domain.ContingentPracticePlanQueries.GetSchedule(year, province);
                return Json(rooms, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult History(string year, string province)
        {
            var rooms = "";//TODO: Domain.ContingentEventHistoryQueries.GetEvents(province);

            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult CreateTeam(CreateTeam command)
        {
            command.TeamId = command.TeamId == Guid.Empty
                ? Guid.NewGuid()
                : command.TeamId;

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult RemoveTeam(RemoveTeam command)
        {
            if (command.TeamId == null || command.TeamId.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignParticipantToContingent(AddParticipantToContingent command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignParticipantToTeam(AddParticipantToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignAlternateToTeam(AssignAlternateToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignCoachToTeam(AddCoachToTeam command)
        {
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult ChangeRoomType(ChangeRoomType command)
        {
            if (command.Id == null || command.Id.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult SaveTravelPlans(SaveTravelPlans command)
        {
            if (command.Id == null || command.Id.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult SavePracticePlan(SavePracticePlan command)
        {
            if (command.Id == null || command.Id.Equals(Guid.Empty))
                return Json(command);

            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult SaveReservationInstructions(SaveReservationInstructions command)
        {
            if (command.Id == null || command.Id.Equals(Guid.Empty))
                return Json(command);
            
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}

