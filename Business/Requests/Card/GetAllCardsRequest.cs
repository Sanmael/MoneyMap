namespace Business.Requests.Card
{
    public class GetAllCardsRequest : BaseRequest
    {        
        public Guid UserId { get; set; }
    }
}