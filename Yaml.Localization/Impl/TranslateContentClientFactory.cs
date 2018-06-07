namespace Xamarin.Yaml.Localization.Impl
{
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    public static class TranslateContentClientFactory
    {
        public static ITranslateContentClient Create(
            AssemblyContentConfig contentConfig)
        {
            return new AssemblyTranslateContentClient(contentConfig);
        }

        public static ITranslateContentClient Create(
            IPlatformComponentsFactory platformComponentsFactory,
            RemoteContentConfig contentConfig)
        {
            return new RemoteTranslateContentClient(platformComponentsFactory, contentConfig);
        }

        public static ITranslateContentClient Create(
            IPlatformComponentsFactory platformComponentsFactory,
            RemoteContentConfig contentConfig,
            OfflineContentConfig offlineContentConfig)
        {
            return new OfflineRemoteTranslateContentClient(platformComponentsFactory, contentConfig, offlineContentConfig);
        }
    }
}