namespace Business.Requests.Card
{
    public class UpdateBalanceCardRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }        
        public decimal Balance { get; set; }
    }
}