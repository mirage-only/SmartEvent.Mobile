using SmartEvent.Mobile.Core.Common;

namespace SmartEvent.Mobile.Infrastructure.Api
{
    public interface IApiClient
    {
        Task<ApiResult<TResponse>> GetAsync<TResponse>(string url);
        Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest payload);
        Task<ApiResult<TResponse>> PostAsync<TResponse>(string url);
        Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest payload);
        Task<ApiResult<TResponse>> DeleteAsync<TResponse>(string url);
    }

}
