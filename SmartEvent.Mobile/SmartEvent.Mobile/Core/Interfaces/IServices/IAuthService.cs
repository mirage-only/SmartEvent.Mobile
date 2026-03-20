using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Responses;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ApiResult<AuthorizeUserResponseDto>> RegisterAsync(RegisterUserRequestDto dto);
        Task<ApiResult<AuthorizeUserResponseDto>> LoginAsync(LoginUserRequestDto dto);
        Task<ApiResult<EmptyResponse>> LogoutAsync();
    }
}
