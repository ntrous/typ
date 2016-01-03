using Microsoft.Practices.Unity;
using TradeYourPhone.Web.Factories;
using TradeYourPhone.Core.Repositories.Implementation;
using TradeYourPhone.Core.Repositories.Interface;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Services.Implementation;
using TradeYourPhone.Web.Controllers;
using System.Data.Entity;
using TradeYourPhone.Core.Models;
using Newtonsoft.Json;
using System;

namespace TradeYourPhone.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Unity
            var container = new UnityContainer();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<DbContext, NavexaMobileEntities>(new PerRequestLifetimeManager());
            container.RegisterType<IPhoneService, PhoneService>();
            container.RegisterType<IQuoteService, QuoteService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IReportingService, ReportingService>();
            container.RegisterType<IConfigurationService, ConfigurationService>();

            container.RegisterType<IController, HomeController>("Home");
            container.RegisterType<IController, AccountController>("Account", new InjectionConstructor());
            container.RegisterType<IController, PhoneMakesController>("PhoneMakes");
            container.RegisterType<IController, PhoneModelsController>("PhoneModels");
            container.RegisterType<IController, PhoneConditionsController>("PhoneConditions");
            container.RegisterType<IController, PhoneConditionPricesController>("PhoneConditionPrices");
            container.RegisterType<IController, PaymentTypesController>("PaymentTypes");
            container.RegisterType<IController, QuoteStatusController>("QuoteStatus");
            container.RegisterType<IController, QuotesController>("Quotes");
            container.RegisterType<IController, PhonesController>("Phones");
            container.RegisterType<IController, EmailController>("Email");
            container.RegisterType<IController, DashboardController>("Dashboard");
            container.RegisterType<IController, ConfigurationController>("Configuration");

            var factory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(factory);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
        }

        protected void Application_Error(object sender, EventArgs e, IEmailService emailService)
        {
            Exception exception = Server.GetLastError();
            Server.ClearError();
            emailService.SendAlertEmailAndLogException("Error caught in global", exception);
        }
    }
}
