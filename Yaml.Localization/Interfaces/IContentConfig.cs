namespace Xamarin.Yaml.Localization.Interfaces
{
    using System;
    using Xamarin.Yaml.Parser;

    public interface IContentConfig
    {
        ParserConfig ParserConfig { get; set; }

        Action<string> Logger { get; set; }
    }
}