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
            Domain.RebuildReadModel(id);
        }

        [Authorize(Users = "Chad")]
        public void RebuildSchedule()
        {
            Domain.RebuildSchedule();
        }

        public ActionResult Reports()
        {
            return View();
        }
    }
}
