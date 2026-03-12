using SmartEvent.Mobile.Core.Common;

namespace SmartEvent.Mobile.Core.Interfaces.IServices;

public interface IUserContext
{
    Guid UserId { get; }
    string UserEmail { get; }
    UserRole UserRole { get; }
    bool IsAuthenticated { get; }
    void SetUser(string token);
    void Clear();
}