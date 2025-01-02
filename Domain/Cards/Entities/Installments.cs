namespace Domain.Cards.Entities
{
    public class Installments
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public bool Paid { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime ReferringMonth { get; set; }
    }
}