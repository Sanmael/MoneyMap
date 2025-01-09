namespace Business.Requests.Card
{
    public class UpdateBalanceCardRequest : BaseRequest
    {
        public Guid CardId { get; set; }        
        public decimal Balance { get; set; }
    }
}