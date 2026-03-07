using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IApiClient _apiClient;
        public EventService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<ApiResult<PagedResult<EventLightDto>>> GetAllEvents(PaginationParams paginationParams)
        {
            string url = ApiRoutes.GetLightEvents;

            var result = await _apiClient.PostAsync<PaginationParams, PagedResult<EventLightDto>>(url, paginationParams);

            return result;
        }
    }
}
