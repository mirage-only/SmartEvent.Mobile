using SmartEvent.Mobile.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.Views;

public partial class AccountPage : ContentPage
{
    public AccountPage(AccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
