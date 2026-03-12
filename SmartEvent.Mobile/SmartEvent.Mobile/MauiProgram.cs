using Microsoft.Extensions.Logging;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using SmartEvent.Mobile.Infrastructure.Services;
using SmartEvent.Mobile.Presentation.ViewModels;
using SmartEvent.Mobile.Presentation.Views;

namespace SmartEvent.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddTransient<AuthHeaderHandler>();

            builder.Services.AddHttpClient<IApiClient, ApiClient>(client =>
                {
                    client.BaseAddress = new Uri(ApiRoutes.BaseUrl);
                })
                .AddHttpMessageHandler<AuthHeaderHandler>();
            
            builder.Services.AddSingleton<IAuthService, AuthService>();
            
            builder.Services.AddSingleton<IEventService, EventService>();

            builder.Services.AddSingleton<ILocalizationService, LocalizationService>();

            builder.Services.AddSingleton<IThemeService, ThemeService>();

            builder.Services.AddTransient<EventsViewModel>();
            builder.Services.AddTransient<EventsPage>();

            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<RegisterPage>();

            builder.Services.AddTransient<LoginViewModel>(); 
            builder.Services.AddTransient<LoginPage>();
                
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<AccountPage>();
            builder.Services.AddTransient<AccountViewModel>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
