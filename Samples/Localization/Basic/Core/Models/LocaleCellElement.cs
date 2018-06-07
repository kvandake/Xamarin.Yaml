namespace Yaml.Localization.Sample.Models
{
    using System.Windows.Input;
    using Xamarin.Yaml.Localization.Interfaces;
    using Yaml.Localization.Sample.ViewModels;

    public class LocaleCellElement : BaseViewModel
    {
        private bool isChecked;

        public LocaleCellElement(ILocale locale)
        {
            this.Locale = locale;
            this.Title = locale.DisplayName;
        }

        public ILocale Locale { get; }

        public string Description => this.Locale.Source;

        public virtual bool IsRemote => false;

        public ICommand Command { get; set; }

        public bool IsChecked
        {
            get => this.isChecked;
            set => this.SetProperty(ref this.isChecked, value);
        }
    }
}