using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class QuoteStatusDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Map(QuoteStatus status)
        {
            ID = status.ID;
            Name = status.QuoteStatusName;
        }
    }
}
