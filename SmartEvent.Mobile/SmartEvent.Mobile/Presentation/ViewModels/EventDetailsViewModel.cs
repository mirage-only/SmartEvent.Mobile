using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Common;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Presentation.Views;
using SmartEvent.Mobile.Resources.Localization;

namespace SmartEvent.Mobile.Presentation.ViewModels;

[QueryProperty(nameof(EventIdString), "id")]
public partial class EventDetailsViewModel : ObservableObject
{
    private readonly IEventService _eventService;
    private readonly IRegistrationService _registrationService;
    private readonly IUserContext _userContext;
    private readonly IAttendanceService _attendanceService;

    [ObservableProperty] private bool _isScanning = true;

    [ObservableProperty] private string _eventIdString;
    [ObservableProperty] private Guid _eventId;
    [ObservableProperty] private EventDetailsDto? _event;

    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private bool _isRegistrationVisible = true;
    [ObservableProperty] private bool _isRegistrationEnabled = true;
    [ObservableProperty] private bool _isAttendEnabled = true;
    [ObservableProperty] private bool _isAttendVisible = false;
    [ObservableProperty] private bool _isParticipantsVisible = true;
    [ObservableProperty] private bool _isQrCodePageVisible = true;

    [ObservableProperty] private string _buttonText = AppResources.EventRegistrationButton;
    [ObservableProperty] private string _buttonColor = "#FF0000FF";

    [ObservableProperty] private string _attendButtonText = AppResources.EventAttendButton;
    [ObservableProperty] private string _attendbuttonColor = "#FF0000FF";

    public EventDetailsViewModel(IEventService eventService, IRegistrationService registrationService, IUserContext userContext, IAttendanceService attendanceService)
    {
        _eventService = eventService;
        _registrationService = registrationService;
        _userContext = userContext;
        _attendanceService = attendanceService;
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

            if (_userContext.UserRole == UserRole.Student)
            {
                IsParticipantsVisible = false;
                IsQrCodePageVisible = false;
            }

            var result = await _eventService.GetEventDetails(id);

            if (result.IsSuccess && result.Data != null)
            {
                Event = result.Data;

                if (Event.CreatorId == _userContext.UserId)
                {
                    IsRegistrationVisible = false;
                    IsAttendVisible = false;
                }
                
                var checkerForRegistered = await _registrationService.IsRegistrationExist(Event.Id);
                var checkerForAttended = await _attendanceService.IsAttendanceExist(Event.Id);
                if (checkerForAttended.IsSuccess)
                {
                    var responceId = checkerForAttended.Data;
                    if (responceId != Guid.Empty)
                    {
                        IsAttendEnabled = false;
                        IsRegistrationVisible = false;
                        AttendButtonText = AppResources.AlreadyAttended;
                    }
                }
                if (checkerForRegistered.IsSuccess)
                {
                    var responseId = checkerForRegistered.Data;
                    if (responseId != Guid.Empty)
                    {
                        IsRegistrationEnabled = false;
                        IsAttendVisible = true;
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

    [RelayCommand]
    private async Task OpenScanner()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "ResultCommand", BarcodeDetectedCommand }
        };
        await Shell.Current.GoToAsync(nameof(QrScannerPage), navigationParameters);
    }

    [RelayCommand]
    private async Task OnBarcodeDetected(string code)
    {
        await Shell.Current.GoToAsync("..");
        _isScanning = false;
        try
        {
            var result = await _attendanceService.ConfirmAsync(EventId, code);
            if (result.IsSuccess)
            {
                IsAttendEnabled = false;
                AttendButtonText = "Посещено";
                IsRegistrationVisible = false;
                await Shell.Current.DisplayAlertAsync(
                    AppResources.Success,
                    AppResources.AttendSuccess,
                    "OK");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync(
                    AppResources.Error,
                    result.Error ?? AppResources.Error,
                    "OK");
            }
        }
        catch
        {
            await Shell.Current.DisplayAlertAsync(AppResources.Error, AppResources.Error, "OK");
        }

    }

    [RelayCommand]
    private async Task OpenParticipants()
    {
        var navParams = new Dictionary<string, object>
        {
            { "EventId", EventId }
        };

        await Shell.Current.GoToAsync(nameof(ParticipantsPage), navParams);
    }

    [RelayCommand]
    private async Task OpenQrGenerator()
    {
        var navParams = new Dictionary<string, object>
        {
            { "EventId", EventIdString }
        };

        await Shell.Current.GoToAsync(nameof(EventQrCodePage), navParams);
    }


}