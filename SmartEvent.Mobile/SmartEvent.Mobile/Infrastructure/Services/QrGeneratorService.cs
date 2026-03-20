using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Infrastructure.Services;

public class QrGeneratorService : IQrGeneratorService
{
    private readonly IApiClient _apiClient;

    public QrGeneratorService(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<ApiResult<EventQrResponseDto>> StartSessionAsync(Guid eventId)
    {
        string url = $"{ApiRoutes.EventQrCodeStartSession}/{eventId}";
        var result = await _apiClient.PostAsync<EventQrResponseDto>(url);
        return result;
    }

    public async Task<ApiResult<bool>> StopSessionAsync(Guid eventId)
    {
        string url = $"{ApiRoutes.EventQrCodeStopSession}/{eventId}";
        var result = await _apiClient.PostAsync<bool>(url);
        return result;
    }

    public async Task<ApiResult<EventQrResponseDto>> GetCurrentCodeAsync(Guid eventId)
    {
        string url = $"{ApiRoutes.EventQrCodeGetCurrent}/{eventId}";
        var result = await _apiClient.GetAsync<EventQrResponseDto>(url);
        return result;
    }
}