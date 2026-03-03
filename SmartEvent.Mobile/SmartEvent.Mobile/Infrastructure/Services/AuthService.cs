using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Responses;
using SmartEvent.Mobile.Core.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiClient _apiClient;
        public AuthService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<string?> Login(LoginUserRequestDto dto)
        {
            //var result = await _apiClient.PostAsync<AuthorizeUserResponseDto>("/login", dto);
            //if (!result.IsSuccess)
            //    throw new Exception(result.Error);
            //if (result.Data == null) 
            //    throw new Exception("Empty response from server");
            //return result.Data.JwtToken;
            return "jwt login here!";
        }

        public async Task<string?> Register(RegisterUserRequestDto dto)
        {
            //var result = await _apiClient.PostAsync<AuthorizeUserResponseDto>("/register", dto);
            //if (!result.IsSuccess)
            //    throw new Exception(result.Error);
            //if (result.Data == null) 
            //    throw new Exception("Empty response from server");
            //return result.Data.JwtToken;
            return "jwt reg here!";
        }
    }
}
