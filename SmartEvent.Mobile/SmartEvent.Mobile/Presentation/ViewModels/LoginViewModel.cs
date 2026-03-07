using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty] 
        private string? email; 

        [ObservableProperty] 
        private string? password;

        [RelayCommand]
        [Obsolete("Obsolete")]
        private async Task Login()
        {
            var result = await _authService.LoginAsync(new LoginUserRequestDto(email, password));

            if (result.IsSuccess)
            {
                Shell.Current.Window?.Page = new AppShell();
            }
            else
            { //TODO: через шо-то другое
                Application.Current?.MainPage?.DisplayAlert("Oops...", result.Error, "OK");
            }
        }

        [RelayCommand] 
        private async Task GoToRegister() 
        { 
            await Shell.Current.GoToAsync("register");
        }
    }
}
