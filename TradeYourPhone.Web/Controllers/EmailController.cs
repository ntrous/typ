using TradeYourPhone.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TradeYourPhone.Web.Controllers
{
    public class EmailController : Controller
    {
        private IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SendEmail(string name, string from, string subject, string message)
        {
            emailService.SendQueryEmail(name, from, subject, message);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}