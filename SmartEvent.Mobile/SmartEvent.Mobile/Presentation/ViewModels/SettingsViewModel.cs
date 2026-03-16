using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Services;
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
        private readonly IThemeService _themeService;
        private bool _isInitialized;

        public SettingsViewModel(ILocalizationService localizationService, IThemeService themeService)
        {
            _localizationService = localizationService;
            _themeService = themeService;

            var savedLanguage = Preferences.Get("language", "ru");
            SelectedLanguage = Languages.First(x => x.Code == savedLanguage);
            var savedTheme = Preferences.Get("theme", "System");
            SelectedTheme = Themes.First(x => x.Code == savedTheme);

            _isInitialized = true;
        }

        [ObservableProperty]
        private LanguageItem selectedLanguage;

        [ObservableProperty]
        private ThemeItem selectedTheme;


        partial void OnSelectedLanguageChanged(LanguageItem value)
        {
            if (!_isInitialized) return;
            Preferences.Set("language", value.Code);
            _localizationService.SetCulture(value.Code);


            Shell.Current.Window?.Page = new AppShell();
        }
        public List<ThemeItem> Themes { get; } = new()
        {
            new ThemeItem { Title = AppResources.ThemeSystem, Code = "System" },
            new ThemeItem { Title = AppResources.ThemeDark, Code = "Dark" },
            new ThemeItem { Title = AppResources.ThemeLight, Code = "Light" }
        };

        public List<LanguageItem> Languages { get; } = new()
        {
        new LanguageItem { Title = "Русский", Code = "ru" },
        new LanguageItem { Title = "English", Code = "en" }
        };


        partial void OnSelectedThemeChanged(ThemeItem value)
        {
            if (!_isInitialized) return;

            Preferences.Set("theme", value.Code);
            _themeService.SetTheme(value.Code);
        }



    }

    public class LanguageItem
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }

    public class ThemeItem
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }


}
