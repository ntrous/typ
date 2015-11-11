using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Utilities;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class EmailService : IEmailService
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
            try
            {
                var myMessage = new SendGridMessage();
                myMessage.From = new MailAddress(from);
                myMessage.AddTo(to);
                myMessage.Subject = subject;
                myMessage.Text = body;

                var username = ConfigurationManager.AppSettings["SendGridUsername"].ToString();
                var pswd = ConfigurationManager.AppSettings["SendGridPassword"].ToString();
                var credentials = new NetworkCredential(username, pswd);
                var transportWeb = new Web(credentials);
                transportWeb.Deliver(myMessage);
            }
            catch (Exception ex)
            {
                Log.LogError("SendEmail Failed!", ex);
            }
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
            try
            {
                string to = ConfigurationManager.AppSettings["QuerySendToEmail"];
                string body = string.Format("Message from: {0}{1}{2}", name, Environment.NewLine, message);
                SendEmail(to, from, subject, body);
            }
            catch (Exception ex)
            {
                Log.LogError("SendQueryEmail Failed!", ex);
            }
        }

        /// <summary>
        /// Sends an email using the specified template
        /// </summary>
        /// <param name="template">Template Guid</param>
        /// <param name="quote">Quote the email is for</param>
        public void SendEmailTemplate(EmailTemplate template, Quote quote)
        {
            try
            {
                var myMessage = new SendGridMessage();
                myMessage.From = new MailAddress(ConfigurationManager.AppSettings["SalesEmail"], "Trade Your Phone");
                myMessage.AddTo(quote.Customer.Email);
                myMessage.Subject = " "; // Gets replaced by template
                myMessage.Text = " "; // Gets replaced by template
                myMessage.Html = " "; // Gets replaced by template
                myMessage.EnableTemplate("<% body %>"); // Gets replaced by template

                // Sort out template here
                if (template == EmailTemplate.QuoteConfirmationSatchel)
                {
                    myMessage = SetupQuoteSubmittedSatchelEmail(myMessage, quote);
                }
                else if(template == EmailTemplate.QuoteConfirmationSelfPost)
                {
                    myMessage = SetupQuoteSubmittedSelfPostEmail(myMessage, quote);
                }
                else if(template == EmailTemplate.SatchelSent)
                {
                    myMessage = SetupSatchelSentEmail(myMessage, quote);
                }
                else if(template == EmailTemplate.Paid)
                {
                    myMessage = SetupPaidEmail(myMessage, quote);
                }
                else if(template == EmailTemplate.Assessing)
                {
                    myMessage = SetupAssessingEmail(myMessage, quote);
                }

                myMessage.EnableTemplateEngine(template.Value);
                var username = ConfigurationManager.AppSettings["SendGridUsername"].ToString();
                var pswd = ConfigurationManager.AppSettings["SendGridPassword"].ToString();
                var credentials = new NetworkCredential(username, pswd);
                var transportWeb = new Web(credentials);
                transportWeb.Deliver(myMessage);
            }
            catch (Exception ex)
            {
                string error = "SendQuoteSubmittedEmail Failed!";
                Log.LogError(error, ex);
                SendAlertEmail(error, ex.Message);
            }
        }

        /// <summary>
        /// Sends an email template to a customer when they submit a Quote
        /// </summary>
        /// <param name="quote"></param>
        private SendGridMessage SetupQuoteSubmittedSatchelEmail(SendGridMessage message, Quote quote)
        {
            message.AddSubstitution(":FirstName", new List<string> { quote.Customer.FirstName });
            message.AddSubstitution(":QuoteRef", new List<string> { quote.QuoteReferenceId });

            // Send an email to the Sales inbox
            SendSalesAlertEmail(quote);

            return message;
        }

        private SendGridMessage SetupQuoteSubmittedSelfPostEmail(SendGridMessage message, Quote quote)
        {
            message.AddSubstitution(":FirstName", new List<string> { quote.Customer.FirstName });
            message.AddSubstitution(":QuoteRef", new List<string> { quote.QuoteReferenceId });

            // Send an email to the Sales inbox
            SendSalesAlertEmail(quote);

            return message;
        }

        private SendGridMessage SetupSatchelSentEmail(SendGridMessage message, Quote quote)
        {
            message.AddSubstitution(":FirstName", new List<string> { quote.Customer.FirstName });
            message.AddSubstitution(":QuoteRef", new List<string> { quote.QuoteReferenceId });
            return message;
        }

        private SendGridMessage SetupPaidEmail(SendGridMessage message, Quote quote)
        {
            message.AddSubstitution(":FirstName", new List<string> { quote.Customer.FirstName });

            // Establish total amount
            decimal? total = quote.Phones.Where(x => x.PhoneStatusId == (int)PhoneStatusEnum.Paid).Sum(t => t.PurchaseAmount);
            message.AddSubstitution(":TotalAmount", new List<string> { total.Value.ToString() });
            message.AddSubstitution(":QuoteRef", new List<string> { quote.QuoteReferenceId });

            return message;
        }

        private SendGridMessage SetupAssessingEmail(SendGridMessage message, Quote quote)
        {
            message.AddSubstitution(":FirstName", new List<string> { quote.Customer.FirstName });
            message.AddSubstitution(":QuoteRef", new List<string> { quote.QuoteReferenceId });

            return message;
        }

        /// <summary>
        /// Sends an email to the Alerts email address
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendAlertEmail(string subject, string body)
        {
            try
            {
                string to = ConfigurationManager.AppSettings["AlertSendToEmail"];
                string from = ConfigurationManager.AppSettings["AlertSendFromEmail"];
                SendEmail(to, from, subject, body);
            }
            catch (Exception ex)
            {
                Log.LogError("SendAlertEmail Failed!", ex);
            }
        }

        /// <summary>
        /// Sends an email to the Alerts email address
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendSalesAlertEmail(Quote quote)
        {
            try
            {
                string to = ConfigurationManager.AppSettings["SalesEmail"];
                string from = ConfigurationManager.AppSettings["SalesEmail"];
                string subject = string.Format("New Quote Submitted: {0}", quote.QuoteReferenceId);
                string body = string.Format("New quote id: {0} | First name: {1} | Last name: {2}\r\n \r\n", quote.QuoteReferenceId, quote.Customer.FirstName, quote.Customer.LastName);

                foreach(var phone in quote.Phones)
                {
                    body += string.Format("Phone: {0} for {1}\r\n", phone.PhoneMake.MakeName +" "+ phone.PhoneModel.ModelName, phone.PurchaseAmount.Value.ToString("C"));
                }

                body += string.Format("{0}Total: {1}", Environment.NewLine, quote.Phones.Sum(p => p.PurchaseAmount).Value.ToString("C"));

                SendEmail(to, from, subject, body);
            }
            catch (Exception ex)
            {
                Log.LogError("SendSalesAlertEmail Failed!", ex);
            }
        }

        /// <summary>
        /// Sends an email and also logs the exception to the log file
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="ex"></param>
        public void SendAlertEmailAndLogException(string subject, MethodBase method, Exception ex, params object[] values)
        {
            try
            {
                ParameterInfo[] parms = method.GetParameters();
                object[] namevalues = new object[2 * parms.Length];

                string msg = "Error in " + method.Name + "(";
                for (int i = 0, j = 0; i < parms.Length; i++, j += 2)
                {
                    msg += "{" + j + "}={" + (j + 1) + "}, ";
                    namevalues[j] = parms[i].Name;
                    if (i < values.Length) namevalues[j + 1] = values[i];
                }
                msg += "exception=" + ex.Message + ")";

                string body = string.Format(msg, namevalues);
                Log.LogError(body);
                string to = ConfigurationManager.AppSettings["AlertSendToEmail"];
                string from = ConfigurationManager.AppSettings["AlertSendFromEmail"];
                SendEmail(to, from, subject, body);
            }
            catch (Exception e)
            {
                Log.LogError("SendAlertEmailAndLogBody Failed!", e);
            }
        }
    }
}
