using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Presentation.Views;
using SmartEvent.Mobile.Presentation.ViewModels;

namespace SmartEvent.Mobile.Infrastructure.Services;

public class QrCodeScannerService : IQrCodeScannerService
{
    private readonly IServiceProvider _services;

    public QrCodeScannerService(IServiceProvider services)
    {
        _services = services;
    }

    public async Task<QRCodeScanResult> ScanAsync()
    {
        var tcs = new TaskCompletionSource<QRCodeScanResult>();

        var scannerPage = _services.GetRequiredService<QrScannerPage>();
        var vm = (QrScannerViewModel)scannerPage.BindingContext;

        Action<QRCodeScanResult>? handler = null;
        handler = (result) =>
        {
            vm.OnResult -= handler;
            tcs.TrySetResult(result);
        };

        vm.OnResult += handler;

        await Shell.Current.Navigation.PushAsync(scannerPage);

        return await tcs.Task;
    }
}