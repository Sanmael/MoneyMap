namespace Business.Requests.Card
{
    public class GetCardRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public Guid CardId { get; set; }
    }
}