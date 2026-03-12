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
    {
        if(string.IsNullOrEmpty(token)) return;
        
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token)) return;
        
        var jwt = handler.ReadJwtToken(token);
        
        var idClaim = jwt.Claims.FirstOrDefault(c => c.Type == "id");
        if (Guid.TryParse(idClaim?.Value, out var id)) UserId = id;
        
        var  emailClaim = jwt.Claims.FirstOrDefault(c => c.Type == "email");
        if (emailClaim != null) UserEmail =  emailClaim.Value;
        
        var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == "role");
        if (Enum.TryParse<UserRole>(roleClaim?.Value, out var role)) UserRole = role; 
    }

    public void Clear()
    {
        UserId = Guid.Empty;
        UserEmail = string.Empty;
        UserRole = UserRole.Guest;
    }
}