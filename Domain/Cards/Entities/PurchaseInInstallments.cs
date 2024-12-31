using Domain.Base.Entities;

namespace Domain.Cards.Entities
{
    public class PurchaseInInstallments : Purchase
    {
        public required Card Card { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
    }
}
