using SmartEvent.Mobile.Core.Interfaces.IServices;
using Microsoft.Maui.Controls;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class ThemeService : IThemeService
    {
        public void SetTheme(string theme)
        {
            switch (theme)
            {
                case "Light":
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;

                case "Dark":
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;

                default:
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    break;
            }
        }
    }
}
