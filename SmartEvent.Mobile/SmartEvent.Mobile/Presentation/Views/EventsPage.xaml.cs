using SmartEvent.Mobile.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.Views
{
    public partial class EventsPage : ContentPage
    {
        public EventsPage(EventsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;

            Loaded += async (_, _) => await vm.LoadEventsCommand.ExecuteAsync(null);
        }
    }

}
