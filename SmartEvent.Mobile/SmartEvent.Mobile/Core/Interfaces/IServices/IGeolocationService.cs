using SmartEvent.Mobile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using SmartEvent.Mobile.Core.DTOs;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IGeolocationService
    {
        Task<AppResult<Location>> GetCurrentLocation();        
        Task<ApiResult<LocationDto>> FindEventLocationByAddress(string address);
    }

}
