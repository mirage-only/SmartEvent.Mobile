using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.DTOs.AttendanceDTOs.Requests
{
    public record AttendanceConfirmRequestDto(Guid EventId, string QrValue, double Longitude, double Latitude);
}
