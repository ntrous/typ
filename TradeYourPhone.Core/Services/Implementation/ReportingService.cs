using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Core.Utilities;
using TradeYourPhone.Core.ViewModels;

namespace TradeYourPhone.Core.Services.Implementation
{
    public class ReportingService : IReportingService
    {
        private IUnitOfWork unitOfWork;

        public ReportingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Dashboard

        /// <summary>
        /// Gets all the data for the Dashboard view
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>DashboardViewModel</returns>
        public DashboardViewModel GetDashboardData(DateTime? from, DateTime? to)
        {
            DateTime dateFrom = from ?? Util.GetAEST(DateTime.Today);
            DateTime dateTo = to ?? Util.GetAEST(DateTime.Today);
            dateTo = dateTo.Date.AddDays(1).AddSeconds(-1);

            DashboardViewModel vm = new DashboardViewModel();
            vm.DateFrom = dateFrom; vm.DateTo = dateTo;
            vm.NoOfCreatedQuotes = GetTotalQuotesCreated(dateFrom, dateTo);
            vm.NoOfFinalisedQuotes = GetTotalFinalisedQuotes(dateFrom, dateTo);
            vm.TotalAmountToPay = GetTotalAmountToBePaid();
            vm.TotalAmountPaid = GetTotalAmountPaid(dateFrom, dateTo);
            vm.NoOfCompletedQuotes = GetTotalCompletedQuotes(dateFrom, dateTo);
            vm.TotalDevicesSold = GetTotalDevicesSold(dateFrom, dateTo);
            vm.TotalIncomeAmount = GetTotalIncomeAmount(dateFrom, dateTo);

            return vm;
        }

        /// <summary>
        /// Get total number of quotes created within the given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private int GetTotalQuotesCreated(DateTime from, DateTime to)
        {
            int total = unitOfWork.QuoteRepository.Get(q => q.CreatedDate >= from && q.CreatedDate <= to).Count();
            return total;
        }

        /// <summary>
        /// Gets the total number of finalised quotes within a given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Total number of finalised quotes</returns>
        private int GetTotalFinalisedQuotes(DateTime from, DateTime to)
        {
            int total = unitOfWork.QuoteRepository.Get(q => (q.QuoteStatusId == (int)QuoteStatusEnum.RequiresSatchel || q.QuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
                                                        && (q.QuoteFinalisedDate >= from && q.QuoteFinalisedDate <= to)).Count();
            return total;
        }

        /// <summary>
        /// Gets the total amount of money that needs to be paid
        /// </summary>
        /// <returns></returns>
        private decimal GetTotalAmountToBePaid()
        {
            decimal total = unitOfWork.QuoteRepository.Get(q => (q.QuoteStatusId == (int)QuoteStatusEnum.RequiresSatchel 
                                                                || q.QuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery
                                                                || q.QuoteStatusId == (int)QuoteStatusEnum.Assessing
                                                                || q.QuoteStatusId == (int)QuoteStatusEnum.ReadyForPayment))
                .Sum(q => q.Phones.Sum(p => p.PurchaseAmount)).Value;
            return total;
        }

        /// <summary>
        /// Gets the total amount paid for the given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private decimal GetTotalAmountPaid(DateTime from, DateTime to)
        {
            decimal total = unitOfWork.PhoneRepository.Get(includeProperties: "PhoneStatusHistories").Where(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Paid && (psh.StatusDate >= from && psh.StatusDate <= to)))
                                                            .Sum(p => p.PurchaseAmount).Value;
            return total;
        }

        /// <summary>
        /// Gets the number of completed quotes within a given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private int GetTotalCompletedQuotes(DateTime from, DateTime to)
        {
            int total = unitOfWork.QuoteRepository.Get(q => q.QuoteStatusId == (int)QuoteStatusEnum.Paid, includeProperties: "QuoteStatusHistories")
                                                    .Where(q => q.QuoteStatusHistories.Any(qsh => (qsh.StatusDate >= from && qsh.StatusDate <= to))).Count();
            return total;
        }

        /// <summary>
        /// Get total number of devices sold for a given range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private int GetTotalDevicesSold(DateTime from, DateTime to)
        {
            int total = unitOfWork.PhoneRepository.Get(p => p.PhoneStatusId == (int)PhoneStatusEnum.Sold, includeProperties: "PhoneStatusHistories")
                                                    .Where(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Sold && (psh.StatusDate >= from && psh.StatusDate <= to))).Count();
            return total;
        }

        /// <summary>
        /// Gets the total amount sold for the given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private decimal GetTotalIncomeAmount(DateTime from, DateTime to)
        {
            decimal total = unitOfWork.PhoneRepository.Get(p => p.PhoneStatusId == (int)PhoneStatusEnum.Sold, includeProperties: "PhoneStatusHistories")
                                                    .Where(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Sold && (psh.StatusDate >= from && psh.StatusDate <= to)))
                                                        .Sum(p => p.SaleAmount).Value;
            return total;
        }

        #endregion
    }
}
