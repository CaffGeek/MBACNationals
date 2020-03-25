using System.Web.Mvc;
using System.Web.Routing;

namespace WebFrontend
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("AdminAppV2");

            routes.MapRoute(
                name: "Home",
                url: "Setup",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Contingent",
                url: "Setup/Contingent/{action}/{year}/{province}",
                defaults: new { controller = "Contingent", action = "Index", year = UrlParameter.Optional, province = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "Setup/Account/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Scores",
                url: "Setup/Scores/{action}/{year}/{division}",
                defaults: new { controller = "Scores", action = "Index", division = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Scores2",
                url: "Setup/Scores/{action}/{id}",
                defaults: new { controller = "Scores", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reports",
                url: "Setup/Admin/Reports/{year}",
                defaults: new { controller = "Admin", action = "Reports" }
            );

            routes.MapRoute(
                name: "Sponsors",
                url: "Setup/Sponsors/List/{year}",
                defaults: new { controller = "Sponsors", action = "List" }
            );

            routes.MapRoute(
                name: "SponsorDelete",
                url: "Setup/Sponsors/Delete/{year}",
                defaults: new { controller = "Sponsors", action = "Delete" }
            );

            routes.MapRoute(
                name: "SponsorReorder",
                url: "Setup/Sponsors/Reorder/{year}",
                defaults: new { controller = "Sponsors", action = "Reorder" }
            );

            routes.MapRoute(
                name: "SponsorImage",
                url: "Setup/Sponsors/Image/{id}",
                defaults: new { controller = "Sponsors", action = "Image" }
            );

            routes.MapRoute(
                name: "NewsDelete",
                url: "Setup/News/Delete/{year}",
                defaults: new { controller = "News", action = "Delete" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Setup/Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Hotels",
                url: "Setup/Hotels/List/{year}",
                defaults: new { controller = "Hotels", action = "List" }
            );

            routes.MapRoute(
                name: "HotelDelete",
                url: "Setup/Hotels/Delete/{year}",
                defaults: new { controller = "Hotels", action = "Delete" }
            );

            routes.MapRoute(
                name: "HotelImage",
                url: "Setup/Hotels/Image/{id}",
                defaults: new { controller = "Hotels", action = "Image" }
            );

            routes.MapRoute(
                name: "HotelLogo",
                url: "Setup/Hotels/Logo/{id}",
                defaults: new { controller = "Hotels", action = "Logo" }
            );

            routes.MapRoute(
                name: "Centres",
                url: "Setup/Centres/List/{year}",
                defaults: new { controller = "Centres", action = "List" }
            );

            routes.MapRoute(
                name: "CentreDelete",
                url: "Setup/Centres/Delete/{year}",
                defaults: new { controller = "Centres", action = "Delete" }
            );

            routes.MapRoute(
                name: "CentreImage",
                url: "Setup/Centres/Image/{id}",
                defaults: new { controller = "Centres", action = "Image" }
            );

            routes.MapRoute(
                name: "GuestPackages",
                url: "Setup/GuestPackages/List/{year}",
                defaults: new { controller = "GuestPackages", action = "List" }
            );

            routes.MapRoute(
                name: "GuestPackageDelete",
                url: "Setup/GuestPackages/Delete/{year}",
                defaults: new { controller = "GuestPackages", action = "Delete" }
            );

            routes.MapRoute(
                name: "TournamentEdit",
                url: "Setup/Tournament/Edit/{year}",
                defaults: new { controller = "Tournament", action = "Edit" }
            );

            routes.MapRoute(
                name: "Tournament",
                url: "Setup/Tournament/{action}/{id}",
                defaults: new { controller = "Tournament", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Participant",
                url: "Setup/Participant/{action}/{id}",
                defaults: new { controller = "Participant", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Help",
                url: "Setup/Help/{action}",
                defaults: new { controller = "Help", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "History",
                url: "Setup/History/{year}",
                defaults: new { controller = "History", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "Setup/{controller}/{action}/{year}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}