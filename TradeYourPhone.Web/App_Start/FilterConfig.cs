using System.Web.Mvc;
using TradeYourPhone.Web.App_Start;

namespace TradeYourPhone.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}