namespace Business.Requests.Purchase
{
    public class GetPurchaseRequest : BaseRequest
    {
        public Guid PurchaseId { get; set; }
    }
}