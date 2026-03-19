using SmartEvent.Mobile.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.Views
{
    public partial class ParticipantsPage : ContentPage
    {
        public ParticipantsPage(ParticipantsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
