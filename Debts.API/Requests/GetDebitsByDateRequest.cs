using Business.Requests;

namespace Debts.API.Requests
{
    public class GetDebitsByDateRequest : BaseRequest
    {
        public Guid UserId { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }        
    }
}