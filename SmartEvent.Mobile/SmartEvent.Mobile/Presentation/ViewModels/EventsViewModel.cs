using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
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
            var list = await _eventsService.GetAllEvents();
            Events = new ObservableCollection<EventLightDto>(list);
        }
    }


}
