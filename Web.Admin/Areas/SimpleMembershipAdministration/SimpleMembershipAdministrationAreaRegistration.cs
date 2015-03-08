using System.Web.Mvc;

namespace WebFrontend.Areas.SimpleMembershipAdministration
{
    public class SimpleMembershipAdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SimpleMembershipAdministration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "SimpleMembershipAdministration_default",
                "SimpleMembershipAdministration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
