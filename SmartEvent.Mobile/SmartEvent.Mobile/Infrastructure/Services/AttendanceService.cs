using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.AttendanceDTOs.Requests;
using SmartEvent.Mobile.Core.DTOs.AttendanceDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IGeolocationService _geo;
        private readonly IApiClient _api;

        public AttendanceService(
            IGeolocationService geo,
            IApiClient api)
        {
            _geo = geo;
            _api = api;
        }

        public async Task<ApiResult<AttendanceConfirmResponseDto>> ConfirmAsync(Guid eventId, string qrCode)
        {
            var geo = await _geo.GetCurrentLocation();
            if (!geo.IsSuccess)
                return ApiResult<AttendanceConfirmResponseDto>.Failure(
                    geo.Error,
                    400,
                    null);


            var dto = new AttendanceConfirmRequestDto(
                eventId,
                qrCode!,
                geo.Value!.Longitude,
                geo.Value!.Latitude
            );
            

            var url = $"{ApiRoutes.EventAttendanceUrl}/{eventId}";
            return await _api.PostAsync<AttendanceConfirmRequestDto, AttendanceConfirmResponseDto>(url, dto);
        }
    }

}
