using System.Text.Json;

namespace Business.Requests
{
    public abstract class BaseRequest
    {
        public string GetSerializedRequest()
        {
            return JsonSerializer.Serialize<object>(this.MemberwiseClone());
        }
    }
}