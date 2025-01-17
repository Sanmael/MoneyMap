namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetPurchaseInInstallmentsRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid PurchaseInInstallmentsId { get; set; }
    }
}