using SmartEvent.Mobile.Core.Common;

namespace SmartEvent.Mobile.Core.Interfaces.IServices;

public interface IRegistrationService
{
    public Task<ApiResult<Guid>> RegisterForEvent(Guid eventId);
}