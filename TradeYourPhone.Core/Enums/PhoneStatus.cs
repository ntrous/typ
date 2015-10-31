using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Enums
{
    public enum PhoneStatusEnum
    {
        WaitingForDelivery = 1,
        Assessing,
        Paid,
        ReturnToCustomer,
        Returned,
        New,
        Cancelled,
        ReadyForSale,
        Listed,
        Sold
    }
}
