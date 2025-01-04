namespace Business.Requests.Card
{
    public class UpdateBalanceCardRequest : BaseRequest
    {
        public long CardId { get; set; }        
        public decimal Balance { get; set; }
    }
}