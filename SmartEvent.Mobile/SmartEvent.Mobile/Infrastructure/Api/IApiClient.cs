using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Api
{
    public interface IApiClient
    {
        Task<ApiResult<T>> GetAsync<T>(string url);
        Task<ApiResult<T>> PostAsync<T>(string url, object body);
        Task<ApiResult<T>> PutAsync<T>(string url, object body);
        Task<ApiResult> DeleteAsync(string url);
    }

}
