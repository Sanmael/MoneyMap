namespace Business.Models
{
    public class CardModel
    {
        public long Id { get; set; }
        public UserModel? User { get; set; }
        public long UserId { get; set; }
        public long CategorieId { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PurchaseCategorieModel? Categorie { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
    }
}