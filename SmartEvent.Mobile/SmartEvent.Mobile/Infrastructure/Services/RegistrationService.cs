using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Infrastructure.Services;

public class RegistrationService: IRegistrationService
{
    private readonly IApiClient _apiClient;
        
    public RegistrationService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }
    
    public async Task<ApiResult<Guid>> RegisterForEvent(Guid eventId)
    {
        string url = $"{ApiRoutes.EventRegistrationUrl}/{eventId}";
        var response = await _apiClient.PostAsync<Guid>(url);
        return response;
    }

    public async Task<ApiResult<Guid>> IsRegistrationExist(Guid eventId)
    {
        string url = $"{ApiRoutes.IsRegistrationExistUrl}/{eventId}";
        var response = await _apiClient.GetAsync<Guid>(url);
        return response;
    }
}