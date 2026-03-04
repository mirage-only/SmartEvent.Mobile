using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
    public class SettingsViewModel
    {
        public List<string> Themes { get; } = new() { "Dark" };
        public List<string> Languages { get; } = new() { "English" };

    }
}
