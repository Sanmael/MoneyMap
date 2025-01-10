namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetAllPurchaseInInstallmentsListActiveByDateRequest : BaseRequest
    {
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
    }
}
