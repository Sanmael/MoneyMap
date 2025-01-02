using Domain.Base.Entities;

namespace Domain.Cards.Entities
{
    public class PurchaseInInstallments : Purchase
    {
        public long CardId { get; set; }
        public Card? Card { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public new decimal TotalPrice => InstallmentValue * NumberOfInstallments;
    }
}