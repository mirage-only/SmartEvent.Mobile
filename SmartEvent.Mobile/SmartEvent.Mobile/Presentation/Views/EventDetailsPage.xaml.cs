using System.ComponentModel;
using Mapsui.Tiling;
using SmartEvent.Mobile.Presentation.ViewModels;

namespace SmartEvent.Mobile.Presentation.Views;

public partial class EventDetailsPage : ContentPage
{
    public EventDetailsPage(EventDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        if (MyMapView?.Map != null)
        {
            MyMapView.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EventDetailsViewModel viewModel)
        {
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            
            if (viewModel.Event != null)
            {
                double latitude = viewModel.Event.Location.Latitude;
                double longitude = viewModel.Event.Location.Longitude;
                string eventName = viewModel.Event.Name;
                    
                DrawMapMarker(latitude, longitude, eventName);
            }
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        
        if (BindingContext is EventDetailsViewModel viewModel)
            viewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(EventDetailsViewModel.Event))
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (BindingContext is EventDetailsViewModel viewModel && viewModel.Event?.Location != null)
                {
                    double latitude = viewModel.Event.Location.Latitude;
                    double longitude = viewModel.Event.Location.Longitude;
                    string eventName = viewModel.Event.Name;

                    DrawMapMarker(latitude, longitude, eventName);
                }
            });
        }
    }

    private void DrawMapMarker(double latitude, double longitude, string eventName)
    {
        const int zoomLevelConst = 16;
        
        MyMapView.Pins.Clear();

        var pin = new Mapsui.UI.Maui.Pin(MyMapView)
        {
            Label = eventName,
            Position = new Mapsui.UI.Maui.Position(latitude, longitude),
            Type = Mapsui.UI.Maui.PinType.Pin,
            Color = Colors.Red
        };
        
        MyMapView.Pins.Add(pin);

        var mapPoint = pin.Position.ToMapsui();
        var zoomLevel = MyMapView.Map.Navigator.Resolutions[zoomLevelConst];
        MyMapView.Map.Navigator.CenterOnAndZoomTo(mapPoint, zoomLevel);
    }
}