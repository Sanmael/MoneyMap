namespace Domain.Entities
{
    public class Installments : BaseEntitie
    {
        public virtual PurchaseInInstallments? PurchaseInInstallments { get; set; }
        public Guid PurchaseInInstallmentsId { get; set; }
        public decimal Value { get; set; }
        public bool Paid { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime ReferringMonth { get; set; }
    }
}