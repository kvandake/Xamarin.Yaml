namespace Xamarin.Yaml.Settings
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class YamlSettingsBuilder
    {
        public static string YamlFileExtension = "yaml";

        private Func<string> getYamlConfigFunc;

        public YamlSettingsBuilder SetConfig(string yamlContent)
        {
            this.getYamlConfigFunc = () => yamlContent;
            return this;
        }

        public YamlSettingsBuilder SetConfig(Assembly assembly, string resourceFolder = "Config", string fileSuffix = "settings")
        {
            this.getYamlConfigFunc = () => FindYamlContentFromAssembly(assembly, resourceFolder, fileSuffix);
            return this;
        }

        public IYamlSettings Build()
        {
            return new YamlSettings(this.getYamlConfigFunc());
        }

        private static string FindYamlContentFromAssembly(Assembly assembly, string resourceFolder, string fileSuffix)
        {
            var resources = assembly
                .GetManifestResourceNames()
                .Where(x => x.Contains($".{resourceFolder}."));
            var supportedResources = resources.Where(name => name.EndsWith(YamlFileExtension) && name.Contains(fileSuffix)).ToList();
            if (supportedResources.Count == 0)
            {
                return string.Empty;
            }

            var configSource = supportedResources.First();
            using (var stream = assembly.GetManifestResourceStream(configSource))
            {
                if (stream == null)
                {
                    throw new Exception($"Not found the config for the source <{configSource}> in assembly:{assembly.FullName}");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}