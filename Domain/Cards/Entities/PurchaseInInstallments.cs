using Domain.Base.Entities;

namespace Domain.Cards.Entities
{
    public class PurchaseInInstallments : Purchase
    {
        public long CardId { get; set; }
        public virtual Card? Card { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public bool Paid { get; set; }
        public virtual List<Installments>? Installments { get; set; }
    }
}