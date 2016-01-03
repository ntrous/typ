using System.Web.Mvc;
using System.Web.Routing;

namespace TradeYourPhone.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SiteB",
                url: "Main",
                defaults: new { controller = "Home", action = "Main", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "service/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "NotFound",
                "{*url}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}