namespace Business.Requests.Card.PurchaseInInstallments
{
    public class GetAllPurchaseInInstallmentsListActiveRequest: BaseRequest
    {
        public Guid UserId { get; set; }
    }
}
