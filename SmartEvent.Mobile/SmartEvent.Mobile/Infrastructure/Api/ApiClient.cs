using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;


namespace SmartEvent.Mobile.Infrastructure.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _http;
        public ApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResult<T>> GetAsync<T>(string url)
        {
            var response = await _http.GetAsync(url);
            return await HandleResponse<T>(response);
        }

        public async Task<ApiResult<T>> PostAsync<T>(string url, object body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PostAsync(url, content);
            return await HandleResponse<T>(response);
        }

        public async Task<ApiResult<T>> PutAsync<T>(string url, object body)
        {
            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _http.PutAsync(url, content);
            return await HandleResponse<T>(response);
        }
        public async Task<ApiResult> DeleteAsync(string url)
        {
            var response = await _http.DeleteAsync(url);
            return await HandleResponse<object>(response);
        }

        private async Task<ApiResult<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var result = new ApiResult<T>
            {
                StatusCode = (int)response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };
            if (response.StatusCode == HttpStatusCode.NoContent)
                return result;
            var json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result.Data = JsonSerializer.Deserialize<T>(json);
            }
            else
            {
                var problem = JsonSerializer.Deserialize<ProblemDetails>(json);
                result.Error = problem?.Detail ?? "Unknown error";
            }
            return result;
        }

    }
}
