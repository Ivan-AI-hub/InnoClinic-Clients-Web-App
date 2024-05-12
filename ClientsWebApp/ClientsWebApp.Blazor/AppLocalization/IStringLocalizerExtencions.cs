using Microsoft.Extensions.Localization;

namespace ClientsWebApp.Blazor.AppLocalization
{
    public static class IStringLocalizerExtencions
    {
        public static LocalizedString GetString(this IStringLocalizer localizer, LocalizationType type, params object[] parameters) 
        {
            return localizer.GetString(type.ToString(), parameters);
        }
    }
}
