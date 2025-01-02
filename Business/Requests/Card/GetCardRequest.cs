namespace Business.Requests.Card
{
    public record GetCardRequest
    {
        public long UserId { get; set; }
        public long CardId { get; set; }
    }
}