using Domain.Base.Entities;

namespace Domain.Cards.Entities
{
    public class Card : BaseEntitie
    {
        public long UserId { get; set; }
        public long CategorieId { get; set; } 
        public User? User { get; set; }
        public PurchaseCategorie? Categorie { get; set; }
        public DateTime DueDate { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
        public List<PurchaseInInstallments>? PurchaseInInstallments { get; set; }

        public Card()
        {
        }

        public bool IsValid()
        {
            return Limit > 0 && Balance >= 0
                && DueDate > DateTime.Now;
        }
    }
}