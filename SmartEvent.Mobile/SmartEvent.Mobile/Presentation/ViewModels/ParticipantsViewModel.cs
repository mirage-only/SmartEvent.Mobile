using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.ParticipantDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels;

[QueryProperty(nameof(EventId), "EventId")]
public partial class ParticipantsViewModel : ObservableObject
{
    private readonly IParticipantService _participantService;

    [ObservableProperty] private Guid _eventId;
    [ObservableProperty] private bool _isBusy;

    [ObservableProperty] private List<EventParticipantDto> _registeredParticipants = new();
    [ObservableProperty] private List<EventParticipantDto> _visitedParticipants = new();
    [ObservableProperty] private List<EventParticipantDto> _currentParticipants = new();

    [ObservableProperty] private string _current = "Registered";

    public ParticipantsViewModel(IParticipantService participantService)
    {
        _participantService = participantService;
    }

    partial void OnEventIdChanged(Guid value)
    {
        if (value != Guid.Empty)
        {
            _ = LoadParticipantsAsync(value);
        }
    }

    private async Task LoadParticipantsAsync(Guid eventId)
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var result = await _participantService.GetParticipants(eventId);

            if (result.IsSuccess && result.Data != null)
            {
                RegisteredParticipants = result.Data.Where(p => p.IsRegistered).ToList();
                VisitedParticipants = result.Data.Where(p => p.IsAttended).ToList();

                if (Current == "Registered")
                    CurrentParticipants = RegisteredParticipants;
                else
                    CurrentParticipants = VisitedParticipants;
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    void ShowRegistered()
    {
        CurrentParticipants = RegisteredParticipants;
        Current = "Registered";
    }

    [RelayCommand]
    void ShowVisited()
    {
        CurrentParticipants = VisitedParticipants;
        Current = "Visited";
    }
}