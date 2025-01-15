namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetPurchaseInInstallmentsListActiveRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
    }
}