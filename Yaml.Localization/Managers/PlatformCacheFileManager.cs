namespace Xamarin.Yaml.Localization.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Xamarin.Yaml.Localization.Interfaces;

    public class PlatformCacheFileManager : IPlatformCacheFileManager
    {
        public static string PersonalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public bool ContainsFile(string filePath)
        {
            return File.Exists(this.WrapPlatform(filePath));
        }

        public IList<string> FindFileNames(string dir)
        {
            return Directory.GetFiles(this.WrapPlatform(dir));
        }

        public async Task UpsertFile(string filePath, string content)
        {
            File.WriteAllText(this.WrapPlatform(filePath), content);
        }

        public Task<string> GetFile(string filePath)
        {
            return Task.FromResult(File.ReadAllText(this.WrapPlatform(filePath)));
        }

        private string WrapPlatform(string filePath)
        {
            return string.IsNullOrEmpty(filePath) ? filePath : Path.Combine(PersonalFolder, filePath);
        }
    }
}