namespace Yaml.Localization.Sample.ViewModels
{
    using System.Reflection;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Localization.Models;
    using Yaml.Localization.Sample.Models;

    public class RemoteTranslateViewModel : BaseTranslateViewModel
    {
        public RemoteTranslateViewModel()
        {
            this.Title = "Remote";
        }

        protected override void ReloadLocale()
        {
            var remoteConfig = new RemoteContentConfig
            {
                Locales =
                {
                    {"ru", "https://raw.githubusercontent.com/kvandake/friendly-locale/master/Sample.Basic/FriendlyLocale.Sample.Core/Locales/ru.yaml"},
                    {"en", "https://raw.githubusercontent.com/kvandake/friendly-locale/master/Sample.Basic/FriendlyLocale.Sample.Core/Locales/en.yaml"}
                }
            };

            // assembly offline locale
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var offlineConfig = OfflineContentConfig.FromAssembly(assembly, "ru.yaml", "Locales");

            I18N.Initialize(remoteConfig, offlineConfig);
            this.ReloadItems();
        }

        protected override LocaleCellElement CreateLocaleCellElement(ILocale locale)
        {
            return locale is RemoteLocale remoteLocale ? new RemoteLocaleCellElement(remoteLocale) : base.CreateLocaleCellElement(locale);
        }
    }
}