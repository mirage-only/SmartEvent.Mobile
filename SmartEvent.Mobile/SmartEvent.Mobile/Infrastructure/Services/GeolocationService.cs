using System.Net;
using Microsoft.Maui.Devices.Sensors;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using SmartEvent.Mobile.Resources.Localization;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IApiClient _apiClient;

        public GeolocationService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public async Task<GeolocationResult> GetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.High,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.GetLocationAsync(request);

                if (location == null)
                    return GeolocationResult.Failure(AppResources.GeolocationErrorUnableToRetrieve);

                return GeolocationResult.Success(location);
            }
            catch (FeatureNotSupportedException)
            {
                return GeolocationResult.Failure(AppResources.GeolocationErrorNotSupported);
            }
            catch (FeatureNotEnabledException)
            {
                return GeolocationResult.Failure(AppResources.GeolocationErrorDisabledGPS);
            }
            catch (PermissionException)
            {
                return GeolocationResult.Failure(AppResources.GeolocationErrorNoPermission);
            }
            catch (Exception ex)
            {
                return GeolocationResult.Failure(AppResources.GeolocationError + " " + ex.Message);
            }
        }

        public async Task<ApiResult<LocationDto>> FindEventLocationByAddress(string address)
        {
            string url = $"{ApiRoutes.GetEventLocationByAddress}/{address}";
            
            var result = await _apiClient.GetAsync<LocationDto>(url);
            
            return result;
        }
    }
}
