namespace Xamarin.Yaml.Localization.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Localization.Models;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    internal class RemoteTranslateContentClient : ITranslateContentClient
    {
        private readonly RemoteContentConfig contentConfig;
        private IPlatformCacheFileManager platformCacheFileManager;
        private IPlatformHttpClientManager platformHttpClientManager;

        public RemoteTranslateContentClient(IPlatformComponentsFactory platformComponentsFactory, RemoteContentConfig contentConfig)
        {
            this.contentConfig = contentConfig;
            this.PlatformComponentsFactory = platformComponentsFactory;
            this.platformCacheFileManager = platformComponentsFactory.CreateCacheFileManager();
        }
        
        public IContentConfig ContentConfig => this.contentConfig;

        protected IPlatformComponentsFactory PlatformComponentsFactory { get; }

        protected IPlatformCacheFileManager PlatformCacheFileManager
            => this.platformCacheFileManager ??
               (this.platformCacheFileManager = this.PlatformComponentsFactory.CreateCacheFileManager());

        protected IPlatformHttpClientManager PlatformHttpClientManager
            => this.platformHttpClientManager ??
               (this.platformHttpClientManager = this.PlatformComponentsFactory.CreateHttpClientManager());

        public virtual async Task<string[]> GetContent(ILocale locale, IProgress<float> progressAction,
            CancellationToken ct = default(CancellationToken))
        {
            var remoteLocale = (RemoteLocale) locale;
            string content;
            if (remoteLocale.Downloaded)
            {
                var filePath = Utils.GetFilePath(this.contentConfig.CacheDir, remoteLocale.CacheSource);
                content = await this.platformCacheFileManager.GetFile(filePath).ConfigureAwait(false);
            }
            else
            {
                var source = remoteLocale.Source;
                content = await this.PlatformHttpClientManager.DownloadContent(source, progressAction, ct).ConfigureAwait(false);
                var cachedFilePath = Utils.GetFilePath(this.contentConfig.CacheDir, remoteLocale.CacheSource);
                await this.platformCacheFileManager.UpsertFile(cachedFilePath, content).ConfigureAwait(false);
                remoteLocale.Downloaded = true;
            }

            return new[] {content};
        }

        public virtual IList<ILocale> GetLocales()
        {
            var locales = new List<ILocale>();
            foreach (var locale in this.contentConfig.Locales)
            {
                var friendlyLocale = new RemoteLocale(locale.Key, locale.Value)
                {
                    CacheFilePrefix = this.contentConfig.CacheFilePrefix
                };

                // set status
                friendlyLocale.Downloaded = this.platformCacheFileManager.ContainsFile(friendlyLocale.CacheSource);

                locales.Add(friendlyLocale);
            }

            return locales;
        }

        public Task<string[]> GetContent(ILocale locale, CancellationToken ct)
        {
            return this.GetContent(locale, null, ct);
        }
    }
}