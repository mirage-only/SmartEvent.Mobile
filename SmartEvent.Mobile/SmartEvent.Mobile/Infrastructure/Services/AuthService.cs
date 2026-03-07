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

        public AuthService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<ApiResult<AuthorizeUserResponseDto>> RegisterAsync(RegisterUserRequestDto dto)
        {
            string url = ApiRoutes.RegisterUrl;
            
            var result = await _apiClient.PostAsync<RegisterUserRequestDto, AuthorizeUserResponseDto>(url, dto);

            if (result.IsSuccess && result.Data != null)
            {
                await SecureStorage.Default.SetAsync("jwt_token", result.Data.JwtToken);
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
                await SecureStorage.Default.SetAsync("jwt_token", result.Data.JwtToken);
            }
            
            return result;
        }

        public Task<ApiResult<EmptyResponse>> LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
