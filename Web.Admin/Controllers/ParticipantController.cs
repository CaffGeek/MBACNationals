using MBACNationals.Participant.Commands;
using System;
using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    public class ParticipantController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View(
        //        new WebFrontend.Models.Participant.Index
        //        {
        //            Participants = Domain.ParticipantQueries.GetParticipants(),
        //        });
        //}

        public ActionResult View(Guid id)
        {
            return View(
                new WebFrontend.Models.Participant.View
                {
                    Participant = Domain.ParticipantQueries.GetParticipant(id),
                });
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Index(Guid? id)
        {
            if (!id.HasValue)
                return Json(null, JsonRequestBehavior.AllowGet);

            var participant = Domain.ParticipantQueries.GetParticipant(id.Value);
            return Json(participant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult All(string year)
        {
            var participants = Domain.ParticipantQueries.GetParticipants(year);
            return Json(participants, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Contingent(string year, string province)
        {
            var participant = Domain.ReservationQueries.GetParticipants(year, province);
            return Json(participant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Profile(Guid id)
        {
            var participantProfile = Domain.ParticipantProfileQueries.GetProfile(id);
            return Json(participantProfile, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public JsonResult Profiles(int year)
        {
            var participantProfile = Domain.ParticipantProfileQueries.GetProfiles(year);
            return Json(participantProfile, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult Create(CreateParticipant command)
        {
            command.Id = Guid.NewGuid();

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult Update(UpdateParticipant command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult AssignToRoom(AssignParticipantToRoom command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult RemoveFromRoom(RemoveParticipantFromRoom command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult Rename(string id, string value)
        {
            var command = new RenameParticipant
            {
                Id = Guid.Parse(id),
                Name = value
            };

            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult Profile(UpdateParticipantProfile command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }

        [HttpPost]
        [RestrictAccessByRouteId]
        public JsonResult UseAlternate(ReplaceParticipant command)
        {
            Domain.Dispatcher.SendCommand(command);

            return Json(command);
        }
    }
}
