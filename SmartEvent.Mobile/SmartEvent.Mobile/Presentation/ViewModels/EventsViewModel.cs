using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using System.Collections.ObjectModel;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels;

public partial class EventsViewModel : ObservableObject
{
    private readonly IEventService _eventsService;

    [ObservableProperty]
    private ObservableCollection<EventLightDto> events = new();

    public EventsViewModel(IEventService eventsService)
    {
        _eventsService = eventsService;
    }

    [RelayCommand]
    private async Task LoadEvents()
    {
        var result = await _eventsService.GetAllEvents(new PaginationParams());

        if (result.Data != null)
        {
            Events = new ObservableCollection<EventLightDto>(result.Data.Items);
        }
    }

    [RelayCommand]
    private async Task GoToDetails(EventLightDto? dto)
    {
        if (dto == null) return;
        
        await Shell.Current.GoToAsync($"EventDetailsPage?id={dto.Id}", true);
    }
}
