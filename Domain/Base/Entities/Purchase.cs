
namespace Domain.Base.Entities
{
    public class Purchase : BaseEntitie
    {
        public Guid UserId { get; set; }
        public virtual User? User { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? CategorieId { get; set; }
        public bool Paid { get; set; } = true;
        public virtual PurchaseCategorie? Categorie { get; set; } 
    }
}