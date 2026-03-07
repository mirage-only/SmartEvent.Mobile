using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IEventService
    {
        Task<ApiResult<PagedResult<EventLightDto>>> GetAllEvents(PaginationParams paginationParams);
    }
}
