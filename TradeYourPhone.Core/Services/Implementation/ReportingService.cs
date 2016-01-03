using System;
using System.Collections.Generic;
using System.Linq;
using TradeYourPhone.Core.Enums;
using TradeYourPhone.Core.Models;
using TradeYourPhone.Core.Repositories.Interface;
using TradeYourPhone.Core.Services.Interface;

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
        /// Get total number of quotes created within the given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetTotalQuotesCreated(DateTime from, DateTime to)
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
        public int GetTotalFinalisedQuotes(DateTime from, DateTime to)
        {
            int total = unitOfWork.QuoteRepository.Get(q => (q.QuoteStatusId == (int)QuoteStatusEnum.RequiresSatchel || q.QuoteStatusId == (int)QuoteStatusEnum.WaitingForDelivery)
                                                        && (q.QuoteFinalisedDate >= from && q.QuoteFinalisedDate <= to)).Count();
            return total;
        }

        /// <summary>
        /// Gets the total amount of money that needs to be paid
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalAmountToBePaid()
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
        public decimal GetTotalAmountPaid(DateTime from, DateTime to)
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
        public int GetTotalCompletedQuotes(DateTime from, DateTime to)
        {
            int total = unitOfWork.QuoteRepository.Get(q => q.QuoteStatusId == (int)QuoteStatusEnum.Paid, includeProperties: "QuoteStatusHistories").Count(q => q.QuoteStatusHistories.Any(qsh => (qsh.StatusDate >= from && qsh.StatusDate <= to)));
            return total;
        }

        /// <summary>
        /// Get total number of devices sold for a given range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public int GetTotalDevicesSold(DateTime from, DateTime to)
        {
            int total = unitOfWork.PhoneRepository.Get(p => p.PhoneStatusId == (int)PhoneStatusEnum.Sold, includeProperties: "PhoneStatusHistories").Count(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Sold && (psh.StatusDate >= from && psh.StatusDate <= to)));
            return total;
        }

        /// <summary>
        /// Gets the total amount sold for the given date range
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public decimal GetTotalIncomeAmount(DateTime from, DateTime to)
        {
            decimal total = unitOfWork.PhoneRepository.Get(p => p.PhoneStatusId == (int)PhoneStatusEnum.Sold, includeProperties: "PhoneStatusHistories")
                                                    .Where(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Sold && (psh.StatusDate >= from && psh.StatusDate <= to)))
                                                        .Sum(p => p.SaleAmount).Value;
            return total;
        }

        /// <summary>
        /// Get total worth of all Phones by the purchase amount
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public decimal GetTotalAssetWorth()
        {
            decimal total = unitOfWork.PhoneRepository.Get((p => p.PhoneStatusId == (int) PhoneStatusEnum.Paid
                                                                 || p.PhoneStatusId == (int) PhoneStatusEnum.Listed
                                                                 || p.PhoneStatusId == (int) PhoneStatusEnum.ReadyForSale)
                                                                    , includeProperties: "PhoneStatusHistories").Sum(p => p.PurchaseAmount).Value;
            return total;
        }

        /// <summary>
        /// Get total of profit made on all sales
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public decimal GetTotalProfit(DateTime from, DateTime to)
        {
            var soldPhones = unitOfWork.PhoneRepository.Get(p => p.PhoneStatusId == (int)PhoneStatusEnum.Sold, includeProperties: "PhoneStatusHistories")
                                                    .Where(p => p.PhoneStatusHistories.Any(psh => psh.PhoneStatusId == (int)PhoneStatusEnum.Sold && (psh.StatusDate >= from && psh.StatusDate <= to)));

            var totalPurchaseAmount = soldPhones.Sum(p => p.PurchaseAmount);
            var totalSoldAmount = soldPhones.Sum(p => p.SaleAmount);
            decimal total = 0;
            if (totalPurchaseAmount != null && totalSoldAmount != null)
            {
                total = totalSoldAmount.Value - totalPurchaseAmount.Value;
            }
            return total;
        }

        /// <summary>
        /// Returns the percentage of Quotes marked as paid.
        /// Uses the noOfQuotes param as the amount of quotes to include in the calculation.
        /// Calculation ignores recent quotes that are still in progress and are still inside expiry period.
        /// </summary>
        /// <param name="noOfQuotes">No of the most recent quotes to use in the calculation</param>
        /// <returns></returns>
        public decimal GetPercentageOfQuotesCompleted(int noOfQuotes)
        {
            IEnumerable<Quote> quotes = unitOfWork.QuoteRepository.Get(q => q.QuoteFinalisedDate != null, quote => quote.OrderByDescending(q => q.QuoteFinalisedDate)).Take(noOfQuotes);

            int totalCompleted = quotes.Count(q => q.QuoteStatusHistories.Any(qsh => qsh.QuoteStatusId == (int)QuoteStatusEnum.Paid));
            int totalExpired = quotes.Count(q => q.QuoteExpiryDate <= DateTime.Now);

            int adjustedTotal = totalExpired + totalCompleted;
            decimal result = decimal.Divide(totalCompleted, adjustedTotal) * 100;
            return result;
        }

        /// <summary>
        /// Returns the percentage of Quotes marked as paid that were satchel quotes.
        /// Uses the noOfQuotes param as the amount of quotes to include in the calculation.
        /// Calculation ignores recent quotes that are still in progress and are still inside expiry period. 
        /// </summary>
        /// <param name="noOfQuotes"></param>
        /// <returns></returns>
        public decimal GetPercentageOfSatchelQuotesCompleted(int noOfQuotes)
        {
            IEnumerable<Quote> quotes = unitOfWork.QuoteRepository.Get(q => q.QuoteFinalisedDate != null && q.PostageMethodId == (int)PostageMethodEnum.Satchel, quote => quote.OrderByDescending(q => q.QuoteFinalisedDate)).Take(noOfQuotes);

            int totalCompleted = quotes.Count(q => q.QuoteStatusHistories.Any(qsh => qsh.QuoteStatusId == (int)QuoteStatusEnum.Paid));
            int totalExpired = quotes.Count(q => q.QuoteExpiryDate <= DateTime.Now);

            int adjustedTotal = totalExpired + totalCompleted;
            decimal result = decimal.Divide(totalCompleted, adjustedTotal) * 100;
            return result;
        }

        #endregion
    }
}
