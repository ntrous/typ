using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Models;

namespace TradeYourPhone.Core.DTO
{
    public class PhoneConditionDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public void Map(PhoneCondition condition)
        {
            ID = condition.ID;
            Name = condition.Condition;
        }
    }
}
