using MBACNationals.Tournament.Commands;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace WebFrontend.Controllers
{
    public class TournamentController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var year = string.Empty;
            var prov = string.Empty;
            
            var roles = Roles.GetRolesForUser(HttpContext.User.Identity.Name);
            foreach (var role in roles)
            {
                int y;
                if (int.TryParse(role, out y) && y > 1900)
                    year = role;

                if (role.Length == 2)
                    prov = role;            
            }

            if (!string.IsNullOrWhiteSpace(year))
                if (!string.IsNullOrWhiteSpace(prov))
                    return RedirectToAction("Edit", "Contingent", new { year = year, province = prov });
                else
                    return RedirectToAction("Edit", "Contingent", new { year = year });

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
