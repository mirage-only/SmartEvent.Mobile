using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Localization;

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
        [Obsolete("Obsolete")]
        public async Task Register()
        {
            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlertAsync(AppResources.Error, AppResources.RegistrationPageErrorDifferentPasswords, "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Firstname) ||
                string.IsNullOrWhiteSpace(Lastname))
            {
                await Shell.Current.DisplayAlertAsync(AppResources.Error, AppResources.RegistrationPageErrorNotAllFields, "OK");
                return;
            }

            if (Password.Length < 6)
            {
                await Shell.Current.DisplayAlertAsync(AppResources.Error, AppResources.RegistrationPageErrorShortPassword, "OK");
                return;
            }
            
            if(Patronymic == null) Patronymic = string.Empty;

            var result = await _authService.RegisterAsync(new RegisterUserRequestDto(Email, Password, Firstname, Lastname, Patronymic));

            if (result.IsSuccess)
            {
                Shell.Current.Window?.Page = new AppShell();
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert(AppResources.Error, result.Error, "OK");
            }
        }

        [RelayCommand]
        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("..");
        }
    }

}
