namespace Xamarin.Yaml.Localization.Configs
{
    using System.Reflection;

    public class OfflineContentConfig : BaseContentConfig
    {
        internal Assembly Assembly { get; set; }

        internal string FileName { get; set; }
        
        internal string ResourceFolder { get; set; }

        public static OfflineContentConfig FromAssembly(Assembly assembly, string fileName, string resourceFolder = null)
        {
            
            return new OfflineContentConfig
            {
                Assembly = assembly,
                FileName = fileName,
                ResourceFolder = resourceFolder
            };
        }
    }
}