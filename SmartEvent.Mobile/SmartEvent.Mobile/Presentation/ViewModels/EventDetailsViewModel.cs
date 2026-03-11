using CommunityToolkit.Mvvm.ComponentModel;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels;

[QueryProperty(nameof(EventIdString), "id")]
public partial class EventDetailsViewModel : ObservableObject
{
    private readonly IEventService _eventService;

    [ObservableProperty] private string _eventIdString;
    
    [ObservableProperty] private Guid _eventId;

    [ObservableProperty] private EventDetailsDto? _event;

    [ObservableProperty] private bool _isBusy;

    public EventDetailsViewModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    partial void OnEventIdStringChanged(string value)
    {
        if (Guid.TryParse(value, out var id))
        {
            EventId = id;
            _ = LoadEventDetailsAsync(id);
        }

    }

    private async Task LoadEventDetailsAsync(Guid id)
    {
        if(IsBusy) return;

        try
        {
            IsBusy = true;

            var result = await _eventService.GetEventDetails(id);

            if (result.IsSuccess && result.Data != null)
            {
                Event = result.Data;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}