using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontend.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [Authorize(Users = "Chad")]
        public ActionResult Rebuild()
        {
            //Domain.RebuildReadModels();
            return View();
        }

        [Authorize(Users = "Chad")]
        public void RebuildModel(string id)
        {
            Domain.RebuildReadModel(id);
        }

        [Authorize(Users = "Chad")]
        public void RebuildSchedule()
        {
            Domain.RebuildSchedule();
        }

        [Authorize(Users = "Chad")]
        public void Assign2014Contingents()
        {
            Domain.Assign2014Contingents();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Reports()
        {
            return View();
        }
    }
}
