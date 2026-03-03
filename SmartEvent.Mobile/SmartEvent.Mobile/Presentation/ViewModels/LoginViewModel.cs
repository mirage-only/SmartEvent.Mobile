using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.DTOs.UserDTOs.Requests;
using SmartEvent.Mobile.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

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
        private async Task Login()
        {
            var token = await _authService.Login(new LoginUserRequestDto
            {
                Email = Email,
                Password = Password
            });
            await SecureStorage.SetAsync("jwt_token", token);
            Shell.Current.Window.Page = new AppShell();
        }

        [RelayCommand] 
        private async Task GoToRegister() 
        { 
            await Shell.Current.GoToAsync("register");
        }
    }
}
