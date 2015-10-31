using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Enums
{
    public sealed class User
    {
        public static readonly User SystemUser = new User("a054bafa-a2e5-4faa-8404-24543bb56fbf");

        private User(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
