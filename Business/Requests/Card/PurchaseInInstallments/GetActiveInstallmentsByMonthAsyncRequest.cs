namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetActiveInstallmentsByMonthAsyncRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }
    }
}