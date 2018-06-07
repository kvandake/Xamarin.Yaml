namespace Yaml.Localization.Sample.ViewModels
{
    using System.Reflection;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Configs;

    public class AssemblyTranslateViewModel : BaseTranslateViewModel
    {
        public AssemblyTranslateViewModel()
        {
            this.Title = "Assembly";
        }

        protected override void ReloadLocale()
        {
            var assemblyConfig = new AssemblyContentConfig(this.GetType().GetTypeInfo().Assembly)
            {
                ResourceFolder = "Locales"
            };

            I18N.Initialize(assemblyConfig);
            this.ReloadItems();
        }
    }
}