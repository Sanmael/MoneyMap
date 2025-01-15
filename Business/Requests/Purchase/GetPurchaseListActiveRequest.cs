namespace Business.Requests.Purchase
{
    public class GetPurchaseListActiveRequest : BaseRequest
    {
        public Guid UserId { get; set; }
    }
}