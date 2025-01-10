using System.Text.Json.Serialization;

namespace Business.Response
{
    public class BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Location { get; set; } = null;

        [JsonIgnore]
        public bool Success { get; set; }

        public BaseResponse(string error)
        {
            Success = false;
            Error = error;
        }

        protected BaseResponse(bool success)
        {
            Success = success;
        }

        protected BaseResponse(bool success, string message)
        {
            Message = message;
            Success = success;
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }

        public BaseResponse(T entitie) : base(true)
        {
            Data = entitie;
        }

        public BaseResponse(T entitie, string message) : base(true, message)
        {
            Data = entitie;
        }

        [JsonConstructor]
        public BaseResponse(T result, bool success = true, string message = "") : base(true, message)
        {
            Data = result;
            Success = success;
            Message = message;
        }
    }

    public static class BaseResponseExtensions
    {
        public static T GetData<T>(this BaseResponse baseResponse)
        {
            if (baseResponse is BaseResponse<T> responseConvert)
                return responseConvert.Data;

            throw new InvalidCastException($"Cannot convert BaseResponse to BaseResponse<{typeof(T).Name}>.");
        }
    }
}