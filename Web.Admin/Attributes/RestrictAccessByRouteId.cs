using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;

namespace WebFrontend.Attributes
{
    public class RestrictAccessByRouteId : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
                return false;

            if (httpContext.User.IsInRole("Admin"))
                return true;

            return false;
            var handler = httpContext.Handler as MvcHandler;
            var contingent = handler.RequestContext.RouteData.Values["province"] as string;

            if (string.IsNullOrWhiteSpace(contingent))
                return true;

            return httpContext.User.IsInRole(contingent);
        }
    }
}