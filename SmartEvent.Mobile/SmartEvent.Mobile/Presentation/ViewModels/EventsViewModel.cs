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
    private readonly IUserContext _userContext;

    [ObservableProperty] private ObservableCollection<EventLightDto> _events = new();
    [ObservableProperty] private bool _isAddEventButtonVisible = true;

    public EventsViewModel(IEventService eventsService, IUserContext userContext)
    {
        _eventsService = eventsService;
        _userContext = userContext;
    }

    [RelayCommand]
    private async Task LoadEvents()
    {
        var result = await _eventsService.GetAllEvents(new PaginationParams());

        if (result.Data != null)
        {
            Events = new ObservableCollection<EventLightDto>(result.Data.Items);
        }

        if (_userContext.UserRole != UserRole.Admin && _userContext.UserRole != UserRole.Employee)
        {
            IsAddEventButtonVisible = false;
        }
    }

    [RelayCommand]
    private async Task GoToDetails(EventLightDto? dto)
    {
        if (dto == null) return;
        
        await Shell.Current.GoToAsync($"EventDetailsPage?id={dto.Id}", true);
    }

    [RelayCommand]
    private async Task GoToAddEvent()
    {
        await Shell.Current.GoToAsync("AddEventPage", true);
    }
}
