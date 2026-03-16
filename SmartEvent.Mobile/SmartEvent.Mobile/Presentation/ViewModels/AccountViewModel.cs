using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.EventDTOs.Responses;

namespace SmartEvent.Mobile.Presentation.ViewModels;

public partial class AccountViewModel : ObservableObject
{
    [ObservableProperty]
    private string? firstName;

    [ObservableProperty]
    private string? email;

    [ObservableProperty] 
    private List<EventLightDto> registeredEvents; 

    [ObservableProperty] 
    private List<EventLightDto> visitedEvents; 

    [ObservableProperty] 
    private List<EventLightDto> currentEvents;

    [ObservableProperty]
    private string current = "Registered";

    public AccountViewModel()
    {
        LoadUser();
    }

    private async void LoadUser()
    {
        //TODO: load from backend
        FirstName = "user";
        Email = "example@mail.com";
        RegisteredEvents = new List<EventLightDto>
            {
                new EventLightDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Тестовое мероприятие 1",
                    Description = "Описание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятия",
                    StartTime = DateTime.Now.AddDays(1),
                    ImageUrl = "https://i.ytimg.com/vi/u47Uvs1g4yI/maxresdefault.jpg"

                },
                new EventLightDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Тестовое мероприятие 2",
                    Description = "Ещё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событие",
                    StartTime = DateTime.Now.AddDays(2),
                    ImageUrl = "https://i.ytimg.com/vi/u47Uvs1g4yI/maxresdefault.jpg"
                },
                new EventLightDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Тестовое мероприятие 3",
                    Description = "Ещё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событиеЕщё одно тестовое событие",
                    StartTime = DateTime.Now.AddDays(2),
                    ImageUrl = ""
                }
            };
        VisitedEvents = new List<EventLightDto>
            {
                new EventLightDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Тестовое мероприятие 0",
                    Description = "Описание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятияОписание тестового мероприятия",
                    StartTime = DateTime.Now.AddDays(1),
                    ImageUrl = "https://i.ytimg.com/vi/u47Uvs1g4yI/maxresdefault.jpg"

                },
            };
        CurrentEvents = RegisteredEvents;
    }

    [RelayCommand]
    private async Task Logout()
    {
        SecureStorage.Remove("jwt_token");

        var window = Application.Current?.Windows.FirstOrDefault();
        if (window != null)
            window.Page = new AuthShell();
    }
    [RelayCommand] void ShowRegistered() { CurrentEvents = RegisteredEvents; Current = "Registered"; }
    [RelayCommand] void ShowVisited() { CurrentEvents = VisitedEvents; Current = "Visited"; }
}
