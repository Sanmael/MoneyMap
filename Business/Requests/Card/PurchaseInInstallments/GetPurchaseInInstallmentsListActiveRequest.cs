namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetPurchaseInInstallmentsListActiveRequest : BaseRequest
    {
        public long CardId { get; set; }
    }
}