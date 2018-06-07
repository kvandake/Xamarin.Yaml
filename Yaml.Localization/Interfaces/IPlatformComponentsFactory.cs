namespace Xamarin.Yaml.Localization.Interfaces
{
    public interface IPlatformComponentsFactory
    {
        IPlatformCacheFileManager CreateCacheFileManager();

        IPlatformHttpClientManager CreateHttpClientManager();
    }
}