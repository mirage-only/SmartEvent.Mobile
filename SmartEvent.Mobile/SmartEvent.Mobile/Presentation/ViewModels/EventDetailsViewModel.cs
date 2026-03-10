using CommunityToolkit.Mvvm.ComponentModel;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels;

[QueryProperty(nameof(EventId), "id")]
public partial class EventDetailsViewModel : ObservableObject
{
    private readonly IEventService _eventService;

    [ObservableProperty] private Guid _eventId;

    [ObservableProperty] private EventDetailsDto? _event;

    [ObservableProperty] private bool _isBusy;

    public EventDetailsViewModel(IEventService eventService)
    {
        _eventService = eventService;
    }

    partial void OnEventIdChanged(Guid value)
    {
        Task.Run(async () => await LoadEventDetailsAsync(value));
    }

    private async Task LoadEventDetailsAsync(Guid eventId)
    {
        if(IsBusy) return;

        try
        {
            IsBusy = true;

            var result = await _eventService.GetEventDetails(eventId);

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