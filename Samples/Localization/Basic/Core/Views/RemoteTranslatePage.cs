namespace Yaml.Localization.Sample.Views
{
    using Xamarin.Forms;
    using Yaml.Localization.Sample.Helpers;
    using Yaml.Localization.Sample.ViewModels;

    public class RemoteTranslatePage : BaseTranslatePage
    {
        public RemoteTranslatePage()
        {
            var clearCache = new ToolbarItem
            {
                Icon = "ic_trash",
                Text = "Clear cache",
                Priority = 0,
                Command = new Command(() =>
                {
                    DependencyService.Get<IPersonalFolderHelper>().ClearCache();
                    this.ViewModel?.Reload();
                })
            };

            this.ToolbarItems.Add(clearCache);
        }

        protected override ITranslateViewModel TranslateViewModel => new RemoteTranslateViewModel();
    }
}