namespace Business.Requests.Card
{
    public record InsertCardRequest
    {
        public long UserId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long CategorieId { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}