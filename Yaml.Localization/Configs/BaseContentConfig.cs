namespace Xamarin.Yaml.Localization.Configs
{
    using System;
    using Xamarin.Yaml.Localization.Interfaces;
    using Xamarin.Yaml.Parser;

    public abstract class BaseContentConfig : IContentConfig
    {
        public ParserConfig ParserConfig { get; set; } = Default;

        public Action<string> Logger { get; set; }

        public static ParserConfig Default => new ParserConfig();
    }
}