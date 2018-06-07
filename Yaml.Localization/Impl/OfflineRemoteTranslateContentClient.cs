namespace Xamarin.Yaml.Localization.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Exceptions;
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Localization.Models;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    internal class OfflineRemoteTranslateContentClient : RemoteTranslateContentClient
    {
        private readonly OfflineContentConfig offlineContentConfig;

        public OfflineRemoteTranslateContentClient(
            IPlatformComponentsFactory platformComponentsFactory,
            RemoteContentConfig contentConfig,
            OfflineContentConfig offlineContentConfig)
            : base(platformComponentsFactory, contentConfig)
        {
            this.offlineContentConfig = offlineContentConfig;
        }

        private async Task<string[]> GetOfflineContent()
        {
            var resourceFolder = this.offlineContentConfig.ResourceFolder;
            var fileName = this.offlineContentConfig.FileName;
            var assemblyFilePath = string.IsNullOrEmpty(resourceFolder) ? fileName : $".{resourceFolder}.{fileName}";
            var assembly = this.offlineContentConfig.Assembly;
            var assemblyResource = assembly.GetManifestResourceNames().FirstOrDefault(x => x.Contains(assemblyFilePath));
            if (string.IsNullOrEmpty(assemblyResource))
            {
                throw new FriendlyTranslateException();
            }

            return new[] {await AssemblyTranslateContentClient.GetAssemblyContent(assembly, assemblyResource)};
        }

        public override Task<string[]> GetContent(ILocale locale, IProgress<float> progressAction,
            CancellationToken ct = default(CancellationToken))
        {
            return locale is OfflineLocale ? this.GetOfflineContent() : base.GetContent(locale, progressAction, ct);
        }

        public override IList<ILocale> GetLocales()
        {
            var locales = base.GetLocales();
            locales.Insert(0, new OfflineLocale(this.offlineContentConfig));
            return locales;
        }
    }
}