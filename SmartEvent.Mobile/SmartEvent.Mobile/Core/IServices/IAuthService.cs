using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.IServices
{
    public interface IAuthService
    {
        Task<string?> Register(RegisterUserRequestDto dto);
        Task<string?> Login(LoginUserRequestDto dto);
    }
}
