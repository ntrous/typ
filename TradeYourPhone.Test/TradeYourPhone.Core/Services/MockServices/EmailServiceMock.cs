using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SendGrid;
using TradeYourPhone.Core.Enums;

namespace TradeYourPhone.Test.TradeYourPhone.Core.Services.MockServices
{
    public class EmailServiceMock : IEmailService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string to, string from, string subject, string body)
        {

        }

        public void SendEmail(SendGridMessage message)
        {
            
        }

        /// <summary>
        /// Sends an email to the general query email address
        /// </summary>
        /// <param name="name"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        public void SendQueryEmail(string name, string from, string subject, string message)
        {

        }

        public void SendEmailTemplate(EmailTemplate template, Quote quote)
        {

        }

        public SendGridMessage BuildTemplateMessage(SendGridMessage message, EmailTemplate template, Quote quote)
        {
            return null;
        }

        /// <summary>
        /// Sends an email to the Alerts email address
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendAlertEmail(string subject, string body)
        {

        }

        /// <summary>
        /// Send an alert to the sales inbox
        /// </summary>
        /// <param name="quote"></param>
        public void SendSalesAlertEmail(Quote quote)
        {

        }

        /// <summary>
        /// Sends an email and Logs the body to the Log
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendAlertEmailAndLogException(string subject, MethodBase method, Exception ex, params object[] values)
        {

        }

        /// <summary>
        /// Sends an email and Logs the body to the Log
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendAlertEmailAndLogException(string subject, Exception ex)
        {

        }
    }
}
