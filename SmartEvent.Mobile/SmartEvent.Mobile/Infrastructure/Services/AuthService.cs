using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class AuthService: IAuthService
    {
        private readonly IApiClient _apiClient;
        private readonly IUserContext _userContext;

        public AuthService(IApiClient apiClient, IUserContext userContext)
        {
            _apiClient = apiClient;
            _userContext = userContext;
        }
        public async Task<ApiResult<AuthorizeUserResponseDto>> RegisterAsync(RegisterUserRequestDto dto)
        {
            string url = ApiRoutes.RegisterUrl;
            
            var result = await _apiClient.PostAsync<RegisterUserRequestDto, AuthorizeUserResponseDto>(url, dto);

            if (result.IsSuccess && result.Data != null)
            {
                var token = result.Data.JwtToken;
                
                await SecureStorage.Default.SetAsync("jwt_token", token);
                _userContext.SetUser(token);
            }
            
            return result;
        }

        public async Task<ApiResult<AuthorizeUserResponseDto>> LoginAsync(LoginUserRequestDto dto)
        {
            string url = ApiRoutes.LoginUrl;
            
            var result = await _apiClient.PostAsync<LoginUserRequestDto, AuthorizeUserResponseDto>(url, dto);

            if (result.IsSuccess && result.Data != null)
            {
                //TODO: create service for work with storage
                var token = result.Data.JwtToken;
                
                await SecureStorage.Default.SetAsync("jwt_token", token);
                _userContext.SetUser(token);
            }
            
            return result;
        }

        public Task<ApiResult<EmptyResponse>> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
