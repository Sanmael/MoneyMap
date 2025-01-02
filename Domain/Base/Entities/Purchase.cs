
namespace Domain.Base.Entities
{
    public class Purchase : BaseEntitie
    {
        public long UserId { get; set; }
        public User? User { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public long? CategorieId { get; set; } 
        public PurchaseCategorie? Categorie { get; set; } 
    }
}