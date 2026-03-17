using SmartEvent.Mobile.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IGeolocationService
    {
        Task<AppResult<Location>> GetCurrentLocation();
    }

}
