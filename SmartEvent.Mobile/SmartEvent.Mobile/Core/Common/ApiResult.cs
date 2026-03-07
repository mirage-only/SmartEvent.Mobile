using System.Net;

namespace SmartEvent.Mobile.Core.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T? Data {get; set;}
        public string? Error { get; set; }
        public IDictionary<string, string[]>? ValidationErrors { get; set; }

        private ApiResult(bool isSuccess, T? data, string? errorMessage, int statusCode,  IDictionary<string, string[]>? validationErrors)
        {
            IsSuccess = isSuccess;
            Data = data;
            Error = errorMessage;
            StatusCode = statusCode;
        }
        
        public static ApiResult<T> Success(T data, HttpStatusCode statusCode) 
            => new (true, data, null, (int)statusCode,  null);
        public static ApiResult<T> Failure(string? errorMessage, int statusCode, IDictionary<string, string[]>? validationErrors) 
            => new (false, default, errorMessage, statusCode, validationErrors);
    }
}
