namespace Xamarin.Yaml.Settings
{
    public interface IYamlSettings
    {   
        string this[string key] { get; }
        
        string CurrentEnvironment { get; }

        void ChangeEnvironment(string environment);
        
        dynamic Current { get; }
    }
}