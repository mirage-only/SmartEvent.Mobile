using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Presentation.Views;

namespace SmartEvent.Mobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(EventDetailsPage), typeof(EventDetailsPage));
        Routing.RegisterRoute(nameof(AddEventPage), typeof(AddEventPage));
        Routing.RegisterRoute(nameof(QrScannerPage), typeof(QrScannerPage));
        Routing.RegisterRoute(nameof(ParticipantsPage), typeof(ParticipantsPage));
        Routing.RegisterRoute(nameof(EventQrCodePage), typeof(EventQrCodePage));
    }
}