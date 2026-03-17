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
        private readonly IQrCodeScannerService _scanner;
        private readonly IGeolocationService _geo;
        private readonly IApiClient _api;

        public AttendanceService(
            IQrCodeScannerService scanner,
            IGeolocationService geo,
            IApiClient api)
        {
            _scanner = scanner;
            _geo = geo;
            _api = api;
        }

        public async Task<ApiResult<AttendanceConfirmResponseDto>> ConfirmAsync(Guid eventId)
        {
            var geo = await _geo.GetCurrentLocation();
            if (!geo.IsSuccess)
                return ApiResult<AttendanceConfirmResponseDto>.Failure(
                    geo.Error,
                    400,
                    null);


            var qr = await _scanner.ScanAsync();
            if (!qr.IsSuccess)
                return ApiResult<AttendanceConfirmResponseDto>.Failure(
                    qr.Error,
                    400,
                    null);


            var dto = new AttendanceConfirmRequestDto(
                eventId,
                qr.Value!,
                geo.Value!.Longitude,
                geo.Value!.Latitude
            );
            

            var url = ApiRoutes.BaseUrl; //TODO
            return await _api.PostAsync<AttendanceConfirmRequestDto, AttendanceConfirmResponseDto>(url, dto);
        }
    }

}
