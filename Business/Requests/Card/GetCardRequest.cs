namespace Business.Requests.Card
{
    public class GetCardRequest : BaseRequest
    {        
        public Guid CardId { get; set; }
    }
}