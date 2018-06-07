namespace Xamarin.Yaml.Localization.Exceptions
{
    using System;
    using Xamarin.Yaml.Parser.Attributes;

    [Preserve(AllMembers = true)]
    public class FriendlyTranslateException : Exception
    {
        public FriendlyTranslateException()
        {
        }

        public FriendlyTranslateException(string message) : base(message)
        {
        }

        public FriendlyTranslateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static FriendlyTranslateException BuilderException(string configName)
        {
            return new FriendlyTranslateException($"{configName} is required");
        }
        
        public static FriendlyTranslateException NotFoundLocale()
        {
            return new FriendlyTranslateException("Not found locale");
        }

    }
}