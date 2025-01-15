namespace Business.Requests.Purchase
{
    public class GetPurchaseRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid PurchaseId { get; set; }
    }
}