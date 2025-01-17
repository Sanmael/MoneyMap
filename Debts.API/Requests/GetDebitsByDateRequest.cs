using Business.Requests;

namespace Debts.API.Requests
{
    public class GetDebitsByDateRequest : BaseRequest
    {        
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }        
    }
}