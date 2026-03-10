using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace SmartEvent.Mobile.Presentation.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ILocalizationService _localizationService;
        private bool _isInitialized;

        public SettingsViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;

            var saved = Preferences.Get("language", "ru");
            SelectedLanguage = Languages.First(x => x.Code == saved);
            SelectedTheme = Themes.First();

            _isInitialized = true;
        }

        [ObservableProperty]
        private LanguageItem selectedLanguage;

        [ObservableProperty]
        private string selectedTheme;


        partial void OnSelectedLanguageChanged(LanguageItem value)
        {
            if (!_isInitialized) return;
            Preferences.Set("language", value.Code);
            _localizationService.SetCulture(value.Code);


            Shell.Current.Window?.Page = new AppShell();
        }
        public List<string> Themes { get; } = new() { "Dark" };
        public List<LanguageItem> Languages { get; } = new()
        {
        new LanguageItem { Title = "Русский", Code = "ru" },
        new LanguageItem { Title = "English", Code = "en" }
        };

    }

    public class LanguageItem
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

}
