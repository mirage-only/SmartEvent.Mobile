using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SmartEvent.Mobile.Core.Common;


namespace SmartEvent.Mobile.Infrastructure.Api;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {PropertyNameCaseInsensitive =  true};

    public Task<ApiResult<TResponse>> GetAsync<TResponse>(string url) =>
        ExecuteAsync<TResponse>(() =>
            httpClient.GetAsync(url));

    public Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest payload) =>
        ExecuteAsync<TResponse>(() 
            =>  httpClient.PostAsJsonAsync(url, payload));

    public Task<ApiResult<TResponse>> PostAsync<TResponse>(string url) => 
        ExecuteAsync<TResponse>(()
            => httpClient.PostAsync(url, null!));
    

    public Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest payload) =>
        ExecuteAsync<TResponse>(() =>
            httpClient.PutAsJsonAsync(url, payload));

    public Task<ApiResult<TResponse>> DeleteAsync<TResponse>(string url) =>
        ExecuteAsync<TResponse>(() =>
            httpClient.DeleteAsync(url));

    private async Task<ApiResult<TResponse>> ExecuteAsync<TResponse>(Func<Task<HttpResponseMessage>> requestAction)
    {
        try
        {
            using var response = await requestAction();

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return ApiResult<TResponse>.Success(default!, response.StatusCode);

                var content = await response.Content.ReadFromJsonAsync<TResponse>(_jsonSerializerOptions);
                return ApiResult<TResponse>.Success(content!, response.StatusCode);
            }
                
            var parsedResponseWithFail = await ParseProblemDetailsAsync<TResponse>(response);
            return parsedResponseWithFail;
        }
        catch (HttpRequestException)
        {
            return ApiResult<TResponse>.Failure("Network error", (int)HttpStatusCode.BadRequest, null);
        }
        catch (TaskCanceledException)
        {
            return ApiResult<TResponse>.Failure("Network error", (int)HttpStatusCode.RequestTimeout, null);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return ApiResult<TResponse>.Failure("Server is sleeping", (int)HttpStatusCode.InternalServerError, null);
        }
    }

    private async Task<ApiResult<T>> ParseProblemDetailsAsync<T>(HttpResponseMessage response)
    {
        try
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(_jsonSerializerOptions);

            if (problemDetails != null)
            {
                var errorMessage = problemDetails.Detail;
                var errorCode = problemDetails.Status;

                return ApiResult<T>.Failure(errorMessage, errorCode, null);
            }
        }
        catch (JsonException)
        {
            return ApiResult<T>.Failure("Bad request format!", (int)HttpStatusCode.BadRequest, null);
        }
            
        return ApiResult<T>.Failure("Unknown server error! Try later)", (int)HttpStatusCode.InternalServerError, null);
    }
}