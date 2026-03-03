using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.DTOs.UserDTOs.Responses
{
    public class AuthorizeUserResponseDto
    {
        public string JwtToken { get; set; } = string.Empty;
    }
}
