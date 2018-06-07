namespace Xamarin.Yaml.Settings
{
    using System.Collections.Generic;
    using Xamarin.Yaml.Parser;

    internal class YamlSettings : IYamlSettings
    {
        private readonly dynamic dynamicSettings;

        public YamlSettings(string yamlContent)
        {
            this.dynamicSettings = Parser.DeserializeContent(yamlContent);
        }

        public string this[string key] => this.CurrentDictionary[key] as string;

        public string CurrentEnvironment { get; private set; }

        public void ChangeEnvironment(string environment)
        {
            this.CurrentEnvironment = environment;
            this.Current = this.CurrentDictionary[environment];
        }

        public dynamic Current { get; private set; }

        private IDictionary<string, dynamic> CurrentDictionary =>  this.dynamicSettings as IDictionary<string, dynamic>;
    }
}