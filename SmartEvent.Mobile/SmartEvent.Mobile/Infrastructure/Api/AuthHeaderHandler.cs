using System.Net.Http.Headers;

namespace SmartEvent.Mobile.Infrastructure.Api;

public class AuthHeaderHandler: DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await SecureStorage.GetAsync("jwt_token");

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        return await base.SendAsync(request, cancellationToken);
    }
}