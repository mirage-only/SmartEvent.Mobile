namespace SmartEvent.Mobile.Core.Common
{
    public class AppResult<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        protected AppResult(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static AppResult<T> Success(T value) => new(true, value, null);
        public static AppResult<T> Failure(string error) => new(false, default, error);
    }

    public class AppResult : AppResult<object?>
    {
        private AppResult(bool isSuccess, string? error) : base(isSuccess, null, error) { }
        public static AppResult Success() => new(true, null);
        public static new AppResult Failure(string error) => new(false, error);
    }
}