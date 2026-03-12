using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Styles.Themes;

public class ThemeService : IThemeService
{
    public void SetTheme(string theme)
    {
        var appResources = Application.Current.Resources;
        var merged = Application.Current.Resources.MergedDictionaries;

        foreach (var dict in merged.ToList())
        {
            if (dict is LightTheme || dict is DarkTheme)
                merged.Remove(dict);
        }


        switch (theme)
        {
            case "Light":
                merged.Add(new LightTheme());
                Application.Current.UserAppTheme = AppTheme.Light;
                break;

            case "Dark":
                merged.Add(new DarkTheme());
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;

            default:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;
        }
    }
}
