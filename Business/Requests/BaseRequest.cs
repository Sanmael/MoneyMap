using System.Text.Json;

namespace Business.Requests
{
    public class BaseRequest
    {
        public Guid UserId { get; set; }       
    }

    public static class BaseRequestExtensions
    {
        public static string GetSerializedRequest(this BaseRequest request)
        {
            return JsonSerializer.Serialize<object>(request);
        }
    }
}