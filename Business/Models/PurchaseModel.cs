namespace Business.Models
{
    public class PurchaseModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual UserModel? User { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? CategorieId { get; set; }
        public bool Paid { get; set; }
        public virtual PurchaseCategorieModel? Categorie { get; set; }
    }
}