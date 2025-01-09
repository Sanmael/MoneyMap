namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetPurchaseInInstallmentsListActiveRequest : BaseRequest
    {
        public Guid CardId { get; set; }
    }
}