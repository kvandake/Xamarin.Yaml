namespace Xamarin.Yaml.Localization.Interfaces
{
    public interface ILocale
    {
        string Key { get; }

        string DisplayName { get; }

        string Source { get; }
    }
}