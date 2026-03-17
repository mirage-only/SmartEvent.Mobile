using SmartEvent.Mobile.Presentation.ViewModels;
using ZXing.Net.Maui;

namespace SmartEvent.Mobile.Presentation.Views;

public partial class QrScannerPage : ContentPage
{
    public QrScannerPage(QrScannerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void CameraView_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        (BindingContext as QrScannerViewModel)?.BarcodeDetectedCommand.Execute(e);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as QrScannerViewModel)?.NotifyCancelled();
    }
}