namespace Xamarin.Yaml.Localization.Impl
{
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Localization.Managers;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    public class PlatformComponentsFactory : IPlatformComponentsFactory
    {
        public IPlatformCacheFileManager CreateCacheFileManager()
        {
            return new PlatformCacheFileManager();
        }

        public IPlatformHttpClientManager CreateHttpClientManager()
        {
            return new PlatformHttpClientManager();
        }
    }
}