namespace Business.Requests.Card
{
    public class InsertCardRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CategorieId { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}