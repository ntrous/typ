using System;

namespace TradeYourPhone.Core.Services.Interface
{
    public interface IReportingService
    {
        int GetTotalQuotesCreated(DateTime from, DateTime to);
        int GetTotalFinalisedQuotes(DateTime from, DateTime to);
        decimal GetTotalAmountToBePaid();
        decimal GetTotalAmountPaid(DateTime from, DateTime to);
        int GetTotalCompletedQuotes(DateTime from, DateTime to);
        int GetTotalDevicesSold(DateTime from, DateTime to);
        decimal GetTotalIncomeAmount(DateTime from, DateTime to);
        decimal GetTotalAssetWorth();
        decimal GetTotalProfit(DateTime from, DateTime to);
    }
}
