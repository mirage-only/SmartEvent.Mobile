using SmartEvent.Mobile.Core.Interfaces.IServices;
using SmartEvent.Mobile.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SmartEvent.Mobile.Infrastructure.Services
{
    public class LocalizationService : ILocalizationService
    {
        public void SetCulture(string culture)
        {
            var cultureInfo = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            AppResources.Culture = cultureInfo;
        }
    }

}
