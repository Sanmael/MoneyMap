namespace Business.Requests.Card
{
    public record UpdateBalanceCardRequest
    {
        public long CardId { get; set; }
        public long UserId { get; set; }
        public decimal Balance { get; set; }
    }
}