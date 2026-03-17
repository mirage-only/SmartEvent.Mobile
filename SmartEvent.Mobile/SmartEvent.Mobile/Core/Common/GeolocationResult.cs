using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.Common
{
    public class GeolocationResult
    {
        public bool IsSuccess { get; }
        public Location? Location { get; }
        public string? Error { get; }

        private GeolocationResult(bool isSuccess, Location? location, string? error)
        {
            IsSuccess = isSuccess;
            Location = location;
            Error = error;
        }

        public static GeolocationResult Success(Location location)
            => new(true, location, null);

        public static GeolocationResult Failure(string error)
            => new(false, null, error);
    }

}
