namespace Business.Requests.Purchase
{
    public class InsertPurchaseRequest : BaseRequest
    {
        public decimal Value { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategorieId { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}