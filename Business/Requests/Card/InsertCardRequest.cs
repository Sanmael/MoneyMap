namespace Business.Requests.Card
{
    public class InsertCardRequest : BaseRequest
    {
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public long CategorieId { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}