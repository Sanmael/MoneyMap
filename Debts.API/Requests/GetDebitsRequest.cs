using Business.Requests;

namespace Debts.API.Requests
{
    public class GetDebitsRequest : BaseRequest
    {
        public Guid UserId { get; set; }
    }
}
