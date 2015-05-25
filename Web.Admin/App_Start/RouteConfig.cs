using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebFrontend
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Contingent",
                url: "Contingent/{action}/{year}/{province}",
                defaults: new { controller = "Contingent", action = "Index", year = UrlParameter.Optional, province = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "Account/{action}",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Scores",
                url: "Scores/{action}/{year}/{division}",
                defaults: new { controller = "Scores", action = "Index", division = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Scores2",
                url: "Scores/{action}/{id}",
                defaults: new { controller = "Scores", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reports",
                url: "Admin/Reports/{year}",
                defaults: new { controller = "Admin", action = "Reports" }
            );

            routes.MapRoute(
                name: "Sponsors",
                url: "Sponsors/List/{year}",
                defaults: new { controller = "Sponsors", action = "List" }
            );

            routes.MapRoute(
                name: "SponsorDelete",
                url: "Sponsors/Delete/{year}",
                defaults: new { controller = "Sponsors", action = "Delete" }
            );

            routes.MapRoute(
                name: "SponsorImage",
                url: "Sponsors/Image/{id}",
                defaults: new { controller = "Sponsors", action = "Image" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Tournament",
                url: "Tournament/{action}/{id}",
                defaults: new { controller = "Tournament", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Participant",
                url: "Participant/{action}/{id}",
                defaults: new { controller = "Participant", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{year}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}