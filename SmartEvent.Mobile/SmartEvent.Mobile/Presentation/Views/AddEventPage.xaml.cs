using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartEvent.Mobile.Presentation.ViewModels;

namespace SmartEvent.Mobile.Presentation.Views;

public partial class AddEventPage : ContentPage
{
    public AddEventPage(AddEventViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}