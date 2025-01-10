namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetActiveInstallmentsByMonthAsyncRequest : BaseRequest
    {
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }
    }
}