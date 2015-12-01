using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneMakeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Map(PhoneMake make)
        {
            ID = make.ID;
            Name = make.MakeName;
        }
    }
}
