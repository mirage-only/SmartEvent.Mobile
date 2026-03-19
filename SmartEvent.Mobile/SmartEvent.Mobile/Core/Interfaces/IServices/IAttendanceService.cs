using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.AttendanceDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.Interfaces.IServices
{
    public interface IAttendanceService
    {
        Task<ApiResult<AttendanceConfirmResponseDto>> ConfirmAsync(Guid eventId, string qrCode);
        public Task<ApiResult<Guid>> IsAttendanceExist(Guid eventId);

    }

}
