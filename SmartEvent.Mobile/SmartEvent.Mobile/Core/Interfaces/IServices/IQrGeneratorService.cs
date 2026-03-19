using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IQrGeneratorService
    {
        Task<ApiResult<EventQrResponseDto>> StartSessionAsync(Guid eventId);
        Task<ApiResult<bool>> StopSessionAsync(Guid eventId);
        Task<ApiResult<EventQrResponseDto>> GetCurrentCodeAsync(Guid eventId);
    }
}
