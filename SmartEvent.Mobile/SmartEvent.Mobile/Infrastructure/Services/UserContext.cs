using CommunityToolkit.Mvvm.ComponentModel;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using System.IdentityModel.Tokens.Jwt;

namespace SmartEvent.Mobile.Infrastructure.Services;

public partial class UserContext: ObservableObject, IUserContext
{
    [ObservableProperty] private Guid _userId =  Guid.Empty;
    [ObservableProperty] private string _userEmail =  string.Empty;
    [ObservableProperty] private UserRole _userRole =   UserRole.Guest;
    
    public bool IsAuthenticated => UserId != Guid.Empty;
    
    public void SetUser(string token)
    }
