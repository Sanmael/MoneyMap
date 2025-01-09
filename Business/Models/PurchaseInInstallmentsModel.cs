namespace Business.Models
{
    public class PurchaseInInstallmentsModel
    {
        public Guid Id { get; set; }
        public Guid CardId { get; set; }
        public int NumberOfInstallments { get; set; }        
        public string? Name { get; set; }
        public bool Paid { get; set; }
        public string? Description { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? CategorieId { get; set; }
        public List<InstallmentsModel> InstallmentsModels { get; set; }
    }

    public class InstallmentsModel
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public Guid PurchaseInInstallmentsId { get; set; }
        public int InstallmentNumber { get; set; }
        public DateTime ReferringMonth { get; set; }        
        public bool Paid { get; set; }
    }

    public class GetPurchaseInInstallmentsListActiveModel
    {
        public decimal TotalDebt { get; set; }
        public List<PurchaseInInstallmentsModel>? PurchaseInInstallmentsModels { get; set; }
        public CardModel CardModel { get; set; }
    }

    public class GetAllPurchaseInInstallmentsListActiveModel
    {
        public decimal TotalDebt { get; set; }
        public List<PurchaseInInstallmentsModel>? PurchaseInInstallmentsModels { get; set; }
        public List<CardModel> CardModels { get; set; }
    }
}