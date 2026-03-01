using Microsoft.Extensions.Logging;
using SmartEvent.Mobile.Core.IServices;
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
            builder.Services.AddHttpClient<IApiClient, ApiClient>();
            builder.Services.AddSingleton<IEventService, EventService>();
            builder.Services.AddSingleton<EventsViewModel>();
            builder.Services.AddSingleton<EventsPage>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
