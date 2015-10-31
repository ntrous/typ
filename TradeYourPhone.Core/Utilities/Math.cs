using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeYourPhone.Core.Utilities
{
    public class Math
    {
        public static decimal CalculatePercentageChange(decimal previous, decimal current)
        {
            if (previous == 0)
                throw new InvalidOperationException();

            var change = current - previous;
            return ((decimal)change / previous) * 100;
        }

        public static bool IsDecimal(string value)
        {
            decimal result;
            return Decimal.TryParse(value, out result);
        }
    }
}
