namespace Yaml.Localization.Sample.Views
{
    using Xamarin.Forms;
    using Yaml.Localization.Sample.ViewModels;

    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            var assemblyTranslatePage = new AssemblyTranslatePage();

            Page assemblyPage = new NavigationPage(assemblyTranslatePage);
            Page remotePage = new NavigationPage(new RemoteTranslatePage());
            assemblyPage.Icon = remotePage.Icon = "tab_locale.png";
            assemblyPage.Title = "Assembly";
            remotePage.Title = "Remote";

            this.Children.Add(assemblyPage);
            this.Children.Add(remotePage);

            var firstPage = this.Children[0];
            this.Title = firstPage.Title;
            (assemblyTranslatePage.BindingContext as ITranslateViewModel)?.Reload();
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            var currentPage = this.CurrentPage is NavigationPage navigationPage ? navigationPage.CurrentPage : this.CurrentPage;
            if (currentPage?.BindingContext is ITranslateViewModel translateViewModel)
            {
                translateViewModel.Reload();
            }

            this.Title = currentPage?.Title ?? string.Empty;
        }
    }
}