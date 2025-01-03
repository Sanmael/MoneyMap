using System.Text.Json.Serialization;

namespace Business.Response
{
    public class BaseResponse
    {
        public List<string>? Errors { get; set; } = null;
        public string? Message { get; set; } = null;

        [JsonIgnore]
        public bool Success { get; set; }

        public BaseResponse(List<string> errors)
        {
            Success = false;
            Errors = errors;
            Message = "Error";
        }

        protected BaseResponse(bool success)
        {
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
    }

    public static class BaseResponseExtensions
    {
        public static T ReturnResult<T>(this BaseResponse baseResponse)
        {
            if (baseResponse is BaseResponse<T> responseConvert)
            {
                return responseConvert.Result;
            }

            throw new InvalidCastException($"Cannot convert BaseResponse to BaseResponse<{typeof(T).Name}>.");
        }
    }
}