using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IReportingService
    {
        /// <summary>
        /// Gets all data for the Dashboard View
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        DashboardViewModel GetDashboardData(DateTime? from, DateTime? to);
    }
}
