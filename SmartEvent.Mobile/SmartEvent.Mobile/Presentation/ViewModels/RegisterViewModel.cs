using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using SmartEvent.Mobile.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty] private string? email;
        [ObservableProperty] private string? password;
        [ObservableProperty] private string? confirmPassword;
        [ObservableProperty] private string? firstname;
        [ObservableProperty] private string? lastname;
        [ObservableProperty] private string? patronymic;

        [RelayCommand]
        public async Task Register()
        {
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlertAsync("Error", "passwords are different", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Firstname) ||
                string.IsNullOrWhiteSpace(Lastname))
            {
                await Shell.Current.DisplayAlertAsync("Error", "fill all fields", "OK");
                return;
            }

            if (Password.Length < 6)
            {
                await Shell.Current.DisplayAlertAsync("Error", "password is less than 6 symbols", "OK");
                return;
            }

            var token = await _authService.Register(new RegisterUserRequestDto
            {
                Email = Email!,
                Password = Password!,
                FirstName = Firstname!,
                LastName = Lastname!,
                Patronymic = Patronymic
            });

            await SecureStorage.SetAsync("jwt_token", token);
            Shell.Current.Window.Page = new AppShell();
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("..");
        }
    }

}
