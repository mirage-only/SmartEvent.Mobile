using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.ParticipantDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IApiClient _apiClient;

        public ParticipantService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResult<List<EventParticipantDto>>> GetParticipants(Guid eventId)
        {
            string url = $"{ApiRoutes.EventParticipants}/{eventId}";
            return await _apiClient.GetAsync<List<EventParticipantDto>>(url);
        }
    }
}
