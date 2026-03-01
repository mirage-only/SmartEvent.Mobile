using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Api
{
    public class ApiResult
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
    }

    public class ApiResult<T> : ApiResult 
    {
        public T? Data { get; set; } 
    }
}
