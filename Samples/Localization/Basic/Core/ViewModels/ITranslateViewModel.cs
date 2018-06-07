namespace Yaml.Localization.Sample.ViewModels
{
    using System.Collections.Generic;
    using Yaml.Localization.Sample.Models;

    public interface ITranslateViewModel
    {
        string Title { get; }

        IList<LocaleCellElement> Items { get; }

        void Reload();
    }
}