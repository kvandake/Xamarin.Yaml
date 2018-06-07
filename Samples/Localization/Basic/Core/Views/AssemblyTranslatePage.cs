namespace Yaml.Localization.Sample.Views
{
    using Yaml.Localization.Sample.ViewModels;

    public class AssemblyTranslatePage : BaseTranslatePage
    {
        protected override ITranslateViewModel TranslateViewModel => new AssemblyTranslateViewModel();
    }
}