using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.ParticipantDTOs.Responses;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IParticipantService
    {
        Task<ApiResult<List<EventParticipantDto>>> GetParticipants(Guid eventId);
    }
}
