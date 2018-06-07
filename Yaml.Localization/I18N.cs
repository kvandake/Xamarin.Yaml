namespace Xamarin.Yaml.Localization
{
    using System;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Impl;
    using Xamarin.Yaml.Localization.Interfaces;

    public static class I18N
    {
        private static Lazy<II18N> instanceLazy;

        public static II18N Instance => instanceLazy.Value;

        private static IPlatformComponentsFactory CreatePlatformComponentsFactory()
        {
            return new PlatformComponentsFactory();
        }

        public static void Initialize(AssemblyContentConfig contentConfig)
        {
            InitializeInternal(TranslateContentClientFactory.Create(contentConfig));
        }

        public static void Initialize(RemoteContentConfig contentConfig)
        {
            InitializeInternal(TranslateContentClientFactory
                .Create(CreatePlatformComponentsFactory(), contentConfig));
        }

        public static void Initialize(RemoteContentConfig contentConfig, OfflineContentConfig offlineContentConfig)
        {
            InitializeInternal(TranslateContentClientFactory
                .Create(CreatePlatformComponentsFactory(), contentConfig, offlineContentConfig));
        }

        public static void Initialize(ITranslateContentClient translateContentClient)
        {
            InitializeInternal(translateContentClient);
        }

        private static void InitializeInternal(ITranslateContentClient translateContentClient)
        {
            instanceLazy = new Lazy<II18N>(() => new I18NProvider(translateContentClient));
        }
    }
}