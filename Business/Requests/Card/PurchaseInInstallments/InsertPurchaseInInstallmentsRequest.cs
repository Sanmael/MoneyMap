namespace Business.Requests.Card.PurchaseInInstallments
{
    public class InsertPurchaseInInstallmentsRequest
    {
        public long CardId { get; set; }
        public long UserId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public long? CategorieId { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public List<CustomizedInstallment>? CustomInstallments { get; set; }

        public decimal GetTotalValue()
        {
            if(CustomInstallments != null)
                return CustomInstallments.Sum(x=> x.Value);

            return InstallmentValue * NumberOfInstallments;
        }
    }

    public class CustomizedInstallment
    {
        public decimal Value { get; set; } 
        public DateTime DueDate { get; set; }
        public bool Paid { get; set; }
        public int InstallmentNumber { get; set; } 
    }
}