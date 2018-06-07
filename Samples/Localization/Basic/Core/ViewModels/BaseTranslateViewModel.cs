namespace Yaml.Localization.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;
    using Xamarin.Yaml.Localization;
    using Xamarin.Yaml.Localization.Interfaces;
    using Yaml.Localization.Sample.Models;

    public abstract class BaseTranslateViewModel : BaseViewModel, ITranslateViewModel
    {
        private IList<LocaleCellElement> items;

        protected II18N TextSource => I18N.Instance;

        public IList<LocaleCellElement> Items
        {
            get => this.items;
            set => this.SetProperty(ref this.items, value);
        }

        public void Reload()
        {
            this.ReloadLocale();
        }

        protected abstract void ReloadLocale();

        protected void ReloadItems()
        {
            var result = new ObservableCollection<LocaleCellElement>();
            foreach (var locale in this.TextSource.GetAvailableLocales())
            {
                var localeCell = this.CreateLocaleCellElement(locale);
                localeCell.Command = new Command(() => this.ChangeLocale(localeCell));
                result.Add(localeCell);
            }

            this.Items = result;
        }

        protected virtual LocaleCellElement CreateLocaleCellElement(ILocale locale)
        {
            return new LocaleCellElement(locale);
        }

        protected async void ChangeLocale(LocaleCellElement cell)
        {
            var selectedCell = this.Items.FirstOrDefault(x => x.IsChecked);
            if (selectedCell != null)
            {
                selectedCell.IsChecked = false;
            }

            if (cell is RemoteLocaleCellElement remoteCell && !remoteCell.Locale.Downloaded)
            {
                remoteCell.IsDownload = true;
                var progress = new Progress<float>();
                progress.ProgressChanged += (s, e) =>
                {
                    Console.WriteLine($"Download progress: {e}");
                    remoteCell.DownloadProgress = e / 100;
                };
                await this.TextSource.ChangeLocale(cell.Locale, progress);
                remoteCell.UpdateDownloadLabel();
                remoteCell.IsDownload = false;
            }
            else
            {
                await this.TextSource.ChangeLocale(cell.Locale);
            }

            cell.IsChecked = true;
        }
    }
}