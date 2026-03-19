using SmartEvent.Mobile.Presentation.ViewModels;
using SmartEvent.Mobile.Resources.Localization;
using System.Windows.Input;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace SmartEvent.Mobile.Presentation.Views;

public partial class QrScannerPage : ContentPage, IQueryAttributable
{
    private CameraBarcodeReaderView _cameraView;

    public static readonly BindableProperty CodeDetectedCommandProperty =
        BindableProperty.Create(
            nameof(CodeDetectedCommand),
            typeof(ICommand),
            typeof(QrScannerPage));

    public ICommand? CodeDetectedCommand
    {
        get => (ICommand?)GetValue(CodeDetectedCommandProperty);
        set => SetValue(CodeDetectedCommandProperty, value);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ResultCommand", out var command))
        {
            CodeDetectedCommand = command as ICommand;
        }
    }

    public QrScannerPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(500);

        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlertAsync(AppResources.Error, AppResources.CameraErrorNoPermission, "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        if (_cameraView == null)
        {
            _cameraView = new CameraBarcodeReaderView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                IsDetecting = true
            };

            _cameraView.BarcodesDetected += OnBarcodesDetected;

            if (Content is Grid mainGrid)
            {
                mainGrid.Children.Insert(0, _cameraView);
            }
        }
        else
        {
            _cameraView.IsDetecting = true;
        }
    }

    private void OnBarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var result = e.Results?.FirstOrDefault();
        if (result == null || string.IsNullOrWhiteSpace(result.Value)) return;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (_cameraView != null && _cameraView.IsDetecting)
            {
                _cameraView.IsDetecting = false;

                if (CodeDetectedCommand?.CanExecute(result.Value) == true)
                {
                    CodeDetectedCommand.Execute(result.Value);
                }
            }
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_cameraView != null) _cameraView.IsDetecting = false;
    }
}