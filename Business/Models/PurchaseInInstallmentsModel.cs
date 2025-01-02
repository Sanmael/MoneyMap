namespace Business.Models
{
    public class PurchaseInInstallmentsModel
    {
        public long CardId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public long? CategorieId { get; set; }
    }
}