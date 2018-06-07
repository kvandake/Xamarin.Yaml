namespace Yaml.Localization.Tests.Units
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using Xamarin.Yaml.Localization.Configs;
    using Xamarin.Yaml.Localization.Exceptions;
    using Xamarin.Yaml.Localization.Impl;
    using Xamarin.Yaml.Localization.Models;

    [TestFixture]
    public class AssemblyTranslateContentClientTests
    {
        private AssemblyTranslateContentClient assemblyTranslateContentClient;

        [SetUp]
        public void SetUp()
        {
            var hostAssembly = this.GetType().Assembly;
            var assemblyConfig = new AssemblyContentConfig(hostAssembly)
            {
                ResourceFolder = "Locales"
            };
            this.assemblyTranslateContentClient = new AssemblyTranslateContentClient(assemblyConfig);
        }

        [Test]
        public async Task Check_DownloadContent()
        {
            // Arrange
            var locales = this.assemblyTranslateContentClient.GetLocales();

            // Act
            var content = await this.assemblyTranslateContentClient.GetContent(locales.First(), null, new CancellationToken());

            // Assert
            Assert.IsNotEmpty(content);
        }

        [Test]
        public async Task NullSource_ThrowNotFound()
        {
            // Arrange & Act & Assert
            Assert.ThrowsAsync<YamlTranslateException>(async () =>
            {
                await AssemblyTranslateContentClient.GetAssemblyContent(this.GetType().Assembly, null);
            });
        }

        [Test]
        public async Task NotFoundEmbeddedResource_ThrowNotFound()
        {
            // Arrange & Act & Assert
            Assert.ThrowsAsync<YamlTranslateException>(async () =>
            {
                await AssemblyTranslateContentClient.GetAssemblyContent(this.GetType().Assembly, "bad.yaml");
            });
        }

        [Test]
        public async Task Check_Multiple_Assemblies()
        {
            // Arrange
            var hostAssembly = this.GetType().Assembly;
            var assemblyConfig = new AssemblyContentConfig(new List<Assembly> {hostAssembly, hostAssembly})
            {
                ResourceFolder = "Locales"
            };
            this.assemblyTranslateContentClient = new AssemblyTranslateContentClient(assemblyConfig);
            var locales = this.assemblyTranslateContentClient.GetLocales();

            // Act
            var contents = await this.assemblyTranslateContentClient.GetContent(locales.First(), null, new CancellationToken());

            // Assert
            Assert.AreEqual(2, contents.Length);
            foreach (var locale in locales)
            {
                Assert.AreEqual(2, (locale as AssemblyLocale)?.HostAssemblies.Count);
            }
        }
    }
}