namespace SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;

public record RegisterUserRequestDto(string Email, string Password,
    string FirstName, string LastName, string Patronymic);