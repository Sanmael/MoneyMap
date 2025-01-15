namespace Domain.Entities
{
    public class PurchaseInInstallments : Purchase
    {
        public Guid CardId { get; set; }
        public virtual Card? Card { get; set; }
        public virtual List<Installments>? Installments { get; set; }
    }
}