using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Enums
{
    public sealed class EmailTemplate
    {
        public static readonly EmailTemplate QuoteConfirmationSatchel = new EmailTemplate("0524909b-8582-48bf-a9ff-686b0e2ef3e8");
        public static readonly EmailTemplate QuoteConfirmationSelfPost = new EmailTemplate("4d04db37-318e-42ca-935c-718f3e74bfbd");
        public static readonly EmailTemplate SatchelSent = new EmailTemplate("9dcc6c9e-68bf-4c49-a677-7213b9aca046");
        public static readonly EmailTemplate Paid = new EmailTemplate("df65e45d-1e9f-4c71-876d-b4ec3ee38b7e");

        private EmailTemplate(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
