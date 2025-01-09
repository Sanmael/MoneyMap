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
        public T Result { get; set; }

        public BaseResponse(T entitie) : base(true)
        {
            Result = entitie;
        }

        public BaseResponse(T entitie, string message) : base(true, message)
        {
            Result = entitie;
        }

        [JsonConstructor]
        public BaseResponse(T result, bool success = true, string message = "") : base(true, message)
        {
            Result = result;
            Success = success;
            Message = message;
        }
    }

    public static class BaseResponseExtensions
    {
        public static T GetEntitie<T>(this BaseResponse baseResponse)
        {
            if (baseResponse is BaseResponse<T> responseConvert)
                return responseConvert.Result;

            throw new InvalidCastException($"Cannot convert BaseResponse to BaseResponse<{typeof(T).Name}>.");
        }
    }
}