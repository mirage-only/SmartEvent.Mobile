using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Requests;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels;

public partial class AddEventViewModel : ObservableObject
{
    private readonly IEventService _eventService;
    private readonly IGeolocationService _geolocationService;
    private readonly IUserContext _userContext;

    public AddEventViewModel(IEventService eventService, IGeolocationService geolocationService, IUserContext userContext)
    {
        _eventService = eventService;
        _geolocationService = geolocationService;
        _userContext = userContext;
    }

    [ObservableProperty] private string _eventName =  string.Empty;
    [ObservableProperty] private string _eventDescription = string.Empty;
    [ObservableProperty] private DateTime _eventStartDate = DateTime.Today;
    [ObservableProperty] private TimeSpan _eventStartTime =  DateTime.Now.TimeOfDay;
    [ObservableProperty] private string _eventLocationString = string.Empty;
    [ObservableProperty] private double _eventLatitude;
    [ObservableProperty] private double _eventLongitude;
    [ObservableProperty] private string _eventRoom = string.Empty;
    [ObservableProperty] private uint _eventQrExpirationTime = 30;
    [ObservableProperty] private string _eventImageUrl =  string.Empty;

    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private bool _isAddingVisible = true;
    [ObservableProperty] private bool _isAddingEnabled = true;
    [ObservableProperty] private bool _isCheckAddressEnabled = true;
    
    
    [ObservableProperty] private string _buttonText = "Создать мероприятие";
    [ObservableProperty] private string _buttonColor = "#FF0000FF";
    
    [ObservableProperty] private string _imageUrlError =  string.Empty;

    [RelayCommand]
    private async Task ConfirmAddressAsync()
    {

        if (string.IsNullOrWhiteSpace(EventLocationString))
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Адресс не может быть пустым!", "ОК");
            return;
        }
        
        IsCheckAddressEnabled = false;
        IsBusy = true;
        
        try
        {
            var result = await _geolocationService.FindEventLocationByAddress(EventLocationString);

            if (result.IsSuccess && result.Data != null)
            {
                EventLocationString = result.Data.Address;
                EventLatitude = result.Data.Latitude;
                EventLongitude = result.Data.Longitude;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await Shell.Current.CurrentPage.DisplayAlertAsync("Ошибка", "Невозможно проверить адрес", "OK");
        }
        finally
        {
            IsBusy = false;
        }

        await Task.Delay(3000);
        IsCheckAddressEnabled = true;
    }
    
    [RelayCommand]
    [Obsolete("Obsolete")]
    private async Task AddEvent()
    {
        if (!await ValidateForm()) return;
        
        ButtonText = "Ожидание";
        ButtonColor = "#FFD3D3D3";

        try
        {
            IsBusy = true;
            DateTime resultDateTime = EventStartDate.Date.Add(EventStartTime);
        
            var addEventDto = new AddEventDto
            {
                Name = EventName,
                Description = EventDescription,
                ImageUrl = EventImageUrl,
                StartTime = resultDateTime,
                Latitude = EventLatitude,
                Longitude = EventLongitude,
                Address = EventLocationString,
                Room = EventRoom,
                QrCodeExpirationTime = EventQrExpirationTime
            };
            
            var result = await _eventService.AddEvent(addEventDto);

            if (result.IsSuccess && result.Data != Guid.Empty)
            {
                ButtonText = "Вы успешно зарегистрированы!";
                ButtonColor = "#FF008000";
                IsAddingEnabled = false;
                
                await Shell.Current.DisplayAlert("Успех", "Мероприятие успешно создано!", "OK");
                await Shell.Current.GoToAsync("..");
            } 
            else
            {
                ButtonText = "Ошибка! Попробовать снова.";
                ButtonColor = "#FFFF0000";
            }
            
            IsBusy = false;
        }
        catch(Exception exception)
        {
            Console.WriteLine(exception.Message);
            ButtonText = "Ошибка! Попробовать снова.";
            ButtonColor = "#FFFF0000";
        }
    }
    
    private async Task<bool> ValidateForm()
    {
        if (string.IsNullOrWhiteSpace(EventName))
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Введите название мероприятия", "OK");
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(EventDescription))
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Введите описание мероприятия", "OK");
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(EventLocationString))
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Укажите адрес мероприятия", "OK");
            return false;
        }
        
        if (EventLatitude == 0 || EventLongitude == 0)
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Нажмите кнопку 'Проверить адрес', чтобы мы нашли его на карте", "OK");
            return false;
        }
        
        if (EventQrExpirationTime == 0)
        {
            await Shell.Current.DisplayAlertAsync("Внимание", "Укажите время истечения QR-кода (больше 0)", "OK");
            return false;
        }
        
        if (!string.IsNullOrWhiteSpace(EventImageUrl))
        {
            bool isUri = Uri.TryCreate(EventImageUrl, UriKind.Absolute, out var uriResult) 
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        
            if (!isUri && !string.IsNullOrWhiteSpace(EventImageUrl))
            {
                await Shell.Current.DisplayAlertAsync("Внимание", "Ссылка на картинку некорректна (должна начинаться с http/https)", "OK");
                return false;
            }
        }

        return true;
    }
}