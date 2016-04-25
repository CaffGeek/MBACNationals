using System;
using System.Web.Mvc;
using WebFrontend.Attributes;

namespace WebFrontend.Controllers
{
    [Authorize(Roles = "Admin, Host, Reports")]
    public class AdminController : Controller
    {
        [Authorize(Users = "Chad")]
        public ActionResult Rebuild()
        {
            return View();
        }

        [Authorize(Users = "Chad")]
        public void RebuildModel(string id)
        {
            if ("All".Equals(id, StringComparison.OrdinalIgnoreCase))
                Domain.RebuildAll();
            else
                Domain.RebuildReadModel(id);
        }

        [Authorize(Users = "Chad")]
        public void RebuildSchedule()
        {
            Domain.RebuildSchedule();
        }

        [Authorize(Users = "Chad")]
        public void RebuildQualifyingPositions()
        {
            Domain.RebuildQualifyingPositions();
        }

        public ActionResult Reports()
        {
            return View();
        }
    }
}
