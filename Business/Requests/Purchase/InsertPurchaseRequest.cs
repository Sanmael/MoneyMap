using System.Text.Json.Serialization;

namespace Business.Requests.Purchase
{
    public class InsertPurchaseRequest : BaseRequest
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategorieId { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}