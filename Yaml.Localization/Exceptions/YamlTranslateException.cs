namespace Xamarin.Yaml.Localization.Exceptions
{
    using System;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    public class YamlTranslateException : Exception
    {
        public YamlTranslateException()
        {
        }

        public YamlTranslateException(string message) : base(message)
        {
        }

        public YamlTranslateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static YamlTranslateException BuilderException(string configName)
        {
            return new YamlTranslateException($"{configName} is required");
        }
        
        public static YamlTranslateException NotFoundLocale()
        {
            return new YamlTranslateException("Not found locale");
        }

    }
}