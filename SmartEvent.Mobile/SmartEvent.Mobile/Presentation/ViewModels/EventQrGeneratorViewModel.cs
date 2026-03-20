using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Localization;
using System.Diagnostics;
using SkiaSharp;
using SkiaSharp.QrCode;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
    [QueryProperty(nameof(EventIdString), "EventId")]
    public partial class EventQrGeneratorViewModel : ObservableObject
    {
        private readonly IQrGeneratorService _qrGeneratorService;
        private CancellationTokenSource? _cts;

        [ObservableProperty] private Guid _eventId;
        [ObservableProperty] private string _eventIdString;
        [ObservableProperty] private string? _qrCodeString;
        [ObservableProperty] private bool _isActive;
        [ObservableProperty] private bool _isBusy;

        [ObservableProperty] private ImageSource? _qrImageSource;

        [ObservableProperty] private string _buttonText = AppResources.StartRegistration;
        [ObservableProperty] private string _buttonColor = "#0000FF"; 

        private int _currentInterval = 5;


        partial void OnEventIdStringChanged(string value)
        {
            if (Guid.TryParse(value, out var guid))
            {
                EventId = guid;
            }
        }

        public EventQrGeneratorViewModel(IQrGeneratorService qrGeneratorService)
        {
            _qrGeneratorService = qrGeneratorService;
        }

        [RelayCommand]
        private async Task ToggleSession()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (IsActive)
                {
                    var result = await _qrGeneratorService.StopSessionAsync(EventId);
                    if (result.IsSuccess)
                    {
                        StopPolling();
                        IsActive = false;
                        QrCodeString = null;

                        ButtonText = AppResources.StartRegistration;
                        ButtonColor = "#0000FF";
                    }
                }
                else
                {
                    var result = await _qrGeneratorService.StartSessionAsync(EventId);
                    if (result.IsSuccess && result.Data != null)
                    {
                        QrCodeString = result.Data.Code;
                        _currentInterval = result.Data.Interval;
                        IsActive = true;

                        GenerateQrImage(result.Data.Code);

                        ButtonText = AppResources.StopRegistration;
                        ButtonColor = "#FF0000";

                        StartPolling();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Toggle error: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void StartPolling()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        int delay = _currentInterval > 0 ? _currentInterval : 5;
                        await Task.Delay(TimeSpan.FromSeconds(delay), token);

                        if (token.IsCancellationRequested) break;

                        var result = await _qrGeneratorService.GetCurrentCodeAsync(EventId);
                        if (result.IsSuccess && result.Data != null)
                        {
                            QrCodeString = result.Data.Code;
                            _currentInterval = result.Data.Interval;
                                GenerateQrImage(result.Data.Code);
                        }
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Polling error: {ex.Message}");
                    try { await Task.Delay(TimeSpan.FromSeconds(5), token); } catch { }
                }
            }, token);
        }

        public void StopPolling()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
        }

        private void GenerateQrImage(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            try
            {
                var qrCodeData = QRCodeGenerator.CreateQrCode(text, ECCLevel.L);

                var info = new SKImageInfo(512, 512);
                using (var surface = SKSurface.Create(info))
                {
                    var canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);

                    canvas.Render(qrCodeData, info.Width, info.Height, SKColors.Black);

                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        var bytes = data.ToArray();
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            QrImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}