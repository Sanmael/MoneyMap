namespace Business.Models
{
    public class PurchaseInInstallmentsModel
    {
        public long CardId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public string? Name { get; set; }
        public bool Paid { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public long? CategorieId { get; set; }
        public List<InstallmentsModel>? InstallmentsModels { get; set; }
    }

    public class InstallmentsModel
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public long PurchaseInInstallmentsId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime ReferringMonth { get; set; }        
        public bool Paid { get; set; }
    }

    public class GetPurchaseInInstallmentsListActiveModel
    {
        public List<PurchaseInInstallmentsModel> PurchaseInInstallmentsModels { get; set; }
        public CardModel CardModel { get; set; }
    }
}