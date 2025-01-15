namespace Domain.Entities
{
    public class Card : BaseEntitie
    {
        public Guid UserId { get; set; }
        public Guid CategorieId { get; set; }
        public virtual User? User { get; set; }
        public virtual PurchaseCategorie? Categorie { get; set; }
        public DateTime DueDate { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Limit { get; set; }
        public decimal Balance { get; set; }
        public virtual List<PurchaseInInstallments>? PurchaseInInstallments { get; set; }

        public bool IsValid()
        {
            return Limit > 0 && Balance >= 0
                && DueDate > DateTime.Now;
        }
    }
}