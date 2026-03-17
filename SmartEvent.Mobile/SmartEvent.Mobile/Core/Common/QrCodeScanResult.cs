using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Core.Common
{
    public class QRCodeScanResult
    {
        public bool IsSuccess { get; }
        public string? Value { get; }
        public string? Error { get; }

        private QRCodeScanResult(bool isSuccess, string? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static QRCodeScanResult Success(string value)
            => new(true, value, null);

        public static QRCodeScanResult Failure(string error)
            => new(false, null, error);
    }
}

