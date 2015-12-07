using TradeYourPhone.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TradeYourPhone.Core.Enums;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendEmail(string to, string from, string subject, string body);

        /// <summary>
        /// Sends an email to the general query email address
        /// </summary>
        /// <param name="name"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        void SendQueryEmail(string name, string from, string subject, string message);

        /// <summary>
        /// Sends an email using the specified template
        /// </summary>
        /// <param name="template">Template Guid</param>
        /// <param name="quote">Quote the email is for</param>
        void SendEmailTemplate(EmailTemplate template, Quote quote);

        /// <summary>
        /// Sends an email to the Alerts email address
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendAlertEmail(string subject, string body);

        /// <summary>
        /// Send an alert email to the sales inbox
        /// </summary>
        /// <param name="quote"></param>
        void SendSalesAlertEmail(Quote quote);

        /// <summary>
        /// Sends an email and Logs the body to the Log
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendAlertEmailAndLogException(string subject, MethodBase method, Exception ex, params object[] values);

        /// <summary>
        /// Sends an email and Logs the body to the Log
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="ex"></param>
        void SendAlertEmailAndLogException(string subject, Exception ex);
    }
}
