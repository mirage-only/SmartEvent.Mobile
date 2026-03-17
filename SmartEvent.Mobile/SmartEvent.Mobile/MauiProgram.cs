using Microsoft.Extensions.Logging;
using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Infrastructure.Api;
using SmartEvent.Mobile.Infrastructure.Services;
using SmartEvent.Mobile.Presentation.ViewModels;
using SmartEvent.Mobile.Presentation.Views;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace SmartEvent.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            var services = builder.Services;
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            services.AddTransient<AuthHeaderHandler>();

            services.AddHttpClient<IApiClient, ApiClient>(client =>
                {
                    client.BaseAddress = new Uri(ApiRoutes.BaseUrl);
                })
                .AddHttpMessageHandler<AuthHeaderHandler>();
            
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<IUserContext, UserContext>();

            services.AddTransient<EventsViewModel>();
            services.AddTransient<EventsPage>();
            
            services.AddTransient<EventDetailsViewModel>();
            services.AddTransient<EventDetailsPage>();

            services.AddTransient<RegisterViewModel>();
            services.AddTransient<RegisterPage>();

            services.AddTransient<LoginViewModel>(); 
            services.AddTransient<LoginPage>();
                
            services.AddTransient<SettingsPage>();
            services.AddTransient<SettingsViewModel>();

            services.AddTransient<AccountPage>();
            services.AddTransient<AccountViewModel>();
            
#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
