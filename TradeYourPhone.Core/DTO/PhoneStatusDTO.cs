using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneStatusDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Map(PhoneStatu status)
        {
            ID = status.Id;
            Name = status.PhoneStatus;
        }
    }
}
