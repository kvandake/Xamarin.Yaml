namespace Yaml.Localization.Tests.Units
{
    using System.IO;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Impl;
    using Xamarin.Yaml.Localization.Models;

    [TestFixture]
    public class OfflineRemoteTranslateContentClientTests
    {
        private OfflineRemoteTranslateContentClient offlineRemoteTranslateContentClient;
        private OfflineContentConfig offlineContentConfig;

        [SetUp]
        public void SetUp()
        {
            var remoteConfig = new RemoteContentConfig
            {
                CacheDir = Path.GetTempPath()
            };

            var hostAssembly = this.GetType().Assembly;
            this.offlineContentConfig = OfflineContentConfig.FromAssembly(hostAssembly, "en.yaml", "Locales");

            this.offlineRemoteTranslateContentClient = new OfflineRemoteTranslateContentClient(
                new PlatformComponentsFactory(),
                remoteConfig,
                this.offlineContentConfig);
        }

        [Test]
        public async Task Chech_OfflineLocale()
        {
            // Arrange
            var offlineLocale = new OfflineLocale(this.offlineContentConfig);
            
            // Act
            var content = await this.offlineRemoteTranslateContentClient.GetContent(offlineLocale, null);

            // Assert
            Assert.IsNotEmpty(content);
        }
    }
}