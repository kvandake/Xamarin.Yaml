namespace Yaml.Settings.Tests
{
    using NUnit.Framework;
    using Xamarin.Yaml.Settings;

    [TestFixture]
    public class YamlSettingsTests
    {
        [Test]
        public void Check_Secrets_TestCase()
        {
            var settings = new YamlSettingsBuilder().SetConfig(this.GetType().Assembly, "Config", "secrets").Build();
            
            // Act & Assert
            settings.ChangeEnvironment("development");
            var devKey1 = settings.Current.key_1;
            Assert.AreEqual("Development key 1", devKey1);
            
            settings.ChangeEnvironment("staging");
            var stagingKey1 = settings.Current.key_1;
            Assert.AreEqual("Staging key 1", stagingKey1);
            
            settings.ChangeEnvironment("production");
            var productionKey1 = settings.Current.key_1;
            Assert.AreEqual("Production key 1", productionKey1);
        }
        
        [Test]
        public void Check_Database_TestCase()
        {
            var settings = new YamlSettingsBuilder().SetConfig(this.GetType().Assembly, "Config", "database").Build();
            
            // Act & Assert
            settings.ChangeEnvironment("development");
            var devDatabase = settings.Current.database_name;
            Assert.AreEqual("dev_database", devDatabase);
            
            settings.ChangeEnvironment("staging");
            var stagingDatabase = settings.Current.database_name;
            Assert.AreEqual("staging_database", stagingDatabase);
            
            settings.ChangeEnvironment("production");
            var productionDatabase = settings.Current.database_name;
            Assert.AreEqual("prod_database", productionDatabase);
        }
    }
}