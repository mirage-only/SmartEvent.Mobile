using Microsoft.Extensions.DependencyInjection;
using SmartEvent.Mobile.Core.Interfaces.IServices;

namespace SmartEvent.Mobile
{
    public partial class App : Application
    {
        public App(ILocalizationService localizationService)
        {
            InitializeComponent();

            var savedLanguage = Preferences.Get("language", "ru");
            localizationService.SetCulture(savedLanguage);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AuthShell());
        }
    }
}