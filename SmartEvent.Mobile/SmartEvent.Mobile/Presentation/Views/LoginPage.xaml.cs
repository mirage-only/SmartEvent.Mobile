using SmartEvent.Mobile.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
