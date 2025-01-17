using System.Text.Json.Serialization;

namespace Business.Requests.Purchase
{
    public class GetPurchaseRequest : BaseRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid PurchaseId { get; set; }
    }
}