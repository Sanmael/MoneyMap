namespace Business.Models
{
    public class CardModel
    {
        public long UserId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Categorie { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}