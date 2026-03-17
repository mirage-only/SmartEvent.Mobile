using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Resources.Localization;
using ZXing.Net.Maui;

namespace SmartEvent.Mobile.Presentation.ViewModels;

public partial class QrScannerViewModel : ObservableObject
{
    public event Action<AppResult<string>>? OnResult;
    private bool _isProcessed = false;

    [RelayCommand]
    private async Task BarcodeDetected(BarcodeDetectionEventArgs e)
    {
        var value = e.Results?.FirstOrDefault()?.Value;
        if (string.IsNullOrWhiteSpace(value) || _isProcessed) return;

        _isProcessed = true;
        OnResult?.Invoke(AppResult<string>.Success(value));

        await MainThread.InvokeOnMainThreadAsync(async () => {
            await Shell.Current.Navigation.PopAsync();
        });
    }

    public void NotifyCancelled()
    {
        if (!_isProcessed)
        {
            _isProcessed = true;
            OnResult?.Invoke(AppResult<string>.Failure(AppResources.QrScanerCancel));
        }
    }
}