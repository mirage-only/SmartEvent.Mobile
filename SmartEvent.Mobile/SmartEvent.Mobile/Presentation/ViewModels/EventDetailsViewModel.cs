using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Localization;

namespace SmartEvent.Mobile.Presentation.ViewModels;

[QueryProperty(nameof(EventIdString), "id")]
public partial class EventDetailsViewModel : ObservableObject
{
    private readonly IEventService _eventService;
    private readonly IRegistrationService _registrationService;
    private readonly IUserContext _userContext;

    [ObservableProperty] private string _eventIdString;
    [ObservableProperty] private Guid _eventId;
    [ObservableProperty] private EventDetailsDto? _event;

    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private bool _isRegistrationVisible = true;
    [ObservableProperty] private bool _isRegistrationEnabled = true;
    
    [ObservableProperty] private string _buttonText = AppResources.EventRegistrationButton;
    [ObservableProperty] private string _buttonColor = "#FF0000FF";

    public EventDetailsViewModel(IEventService eventService, IRegistrationService registrationService, IUserContext userContext)
    {
        _eventService = eventService;
        _registrationService = registrationService;
        _userContext = userContext;
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

                if (Event.CreatorId == _userContext.UserId) IsRegistrationVisible = false;
                
                var checkerForRegistered = await _registrationService.IsRegistrationExist(Event.Id);
                if (checkerForRegistered.IsSuccess)
                {
                    var responseId = checkerForRegistered.Data;
                    if (responseId != Guid.Empty)
                    {
                        IsRegistrationEnabled = false;
                        ButtonText = AppResources.EventRegistrationAlreadyRegistered;
                        ButtonColor = "#FF008000";
                    }
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Register()
    {
        ButtonText = AppResources.WaitLabel;
        ButtonColor = "#FFD3D3D3";

        try
        {
            var result = await _registrationService.RegisterForEvent(EventId);

            if (result.IsSuccess)
            {
                ButtonText = AppResources.EventRegistrationSuccess;
                ButtonColor = "#FF008000";
                IsRegistrationEnabled = false;
            }
            else
            {
                ButtonText = AppResources.EventRegistrationError;
                ButtonColor = "#FFFF0000";
            }
        }
        catch(Exception exception)
        {
            Console.WriteLine(exception);
            ButtonText = AppResources.EventRegistrationError;
            ButtonColor = "#FFFF0000";
        }
    } 
}