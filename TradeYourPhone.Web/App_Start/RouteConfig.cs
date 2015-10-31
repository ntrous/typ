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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller= "Home|Account|Email|PaymentTypes|PhoneConditionPrices|PhoneConditions|PhoneMakes|PhoneModels|Phones|Quotes|QuoteStatus|HtmlSnapshot|PriceScraper|Dashboard" }
            );

            routes.MapRoute(
                "NotFound",
                "{*url}",
                new { controller = "Home", action = "Index" }
            );
        }
    }
}