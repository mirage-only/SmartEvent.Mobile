using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Requests;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IEventService
    {
        Task<ApiResult<PagedResult<EventLightDto>>> GetAllEvents(PaginationParams paginationParams);
        
        Task<ApiResult<EventDetailsDto>> GetEventDetails(Guid id);
        
        Task<ApiResult<Guid>> AddEvent(AddEventDto addEventDto);
    }
}
