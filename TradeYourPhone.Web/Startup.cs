using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TradeYourPhone.Web.Startup))]
namespace TradeYourPhone.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
