using Xamarin.Forms;
using Yaml.Localization.Sample.Droid.Impl;

[assembly: Dependency(typeof(PersonalFolderHelper))]

namespace Yaml.Localization.Sample.Droid.Impl
{
    using System;
    using System.IO;
    using System.Linq;
    using Xamarin.Yaml.Localization.Impl;
    using Yaml.Localization.Sample.Helpers;

    public class PersonalFolderHelper : IPersonalFolderHelper
    {
        public void ClearCache()
        {
            var fileExt = I18NProvider.YamlFileExtension;
            foreach (var file in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                $"*.{fileExt}").Where(item => item.EndsWith($".{fileExt}")))
            {
                File.Delete(file);
            }
        }
    }
}