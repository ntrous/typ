namespace TradeYourPhone.Core.Enums
{
    public enum QuoteStatusEnum
    {
        New = 1,
        WaitingForDelivery,
        RequiresSatchel,
        Assessing = 6,
        Paid,
        ReturnToCustomer,
        Returned,
        Cancelled = 11,
        ReadyForPayment = 16
    }
}
