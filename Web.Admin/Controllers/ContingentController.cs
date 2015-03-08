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
        public ActionResult Reservation(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Edit(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Arrivals(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Practice(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [RestrictAccessByRouteId] //Province
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Profiles(string province)
        {
            if (string.IsNullOrWhiteSpace(province))
                return View("_ProvinceSelector");

            ViewBag.Province = province;
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Index(string province)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var contingent = Domain.ContingentViewQueries.GetContingent(province);

            if (contingent != null)
                return Json(contingent, JsonRequestBehavior.AllowGet);
            
            var command = new MBACNationals.Contingent.Commands.CreateContingent();
            command.Id = Guid.NewGuid();
            command.Province = province;
            Domain.Dispatcher.SendCommand(command);
            contingent = Domain.ContingentViewQueries.GetContingent(province);

            return Json(contingent, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Team(string contingent, string teamName)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            var theContingent = Domain.ContingentViewQueries.GetContingent(contingent);
            if (theContingent == null)
                return null; //TODO: Return an error???

            return Json(theContingent.Teams.SingleOrDefault(x => x.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase)), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult TravelPlans(string province)
        {
            var travelPlans = Domain.ContingentTravelPlanQueries.GetTravelPlans(province);

            return Json(travelPlans, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Rooms(string province)
        {
            var rooms = Domain.ContingentTravelPlanQueries.GetRooms(province);

            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult PracticePlan(string province)
        {
            var rooms = Domain.ContingentPracticePlanQueries.GetSchedule(province);

            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult History(string province)
        {
            var rooms = Domain.ContingentEventHistoryQueries.GetEvents(province);

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
            var contingent = Domain.ContingentViewQueries.GetContingent(command.Province);

            if (contingent == null)
                return Json(command);

            command.Id = contingent.Id;
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
            var contingent = Domain.ContingentViewQueries.GetContingent(command.Province);

            if (contingent == null)
                return Json(command);

            command.Id = contingent.Id;
            
            Domain.Dispatcher.SendCommand(command);
            return Json(command);
        }
    }
}

