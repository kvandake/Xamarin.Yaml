namespace Xamarin.Yaml.Parser.Translators
{
    using Xamarin.Yaml.Parser.Core;
    using Xamarin.Yaml.Parser.Exceptions;
    using Xamarin.Yaml.Parser.Nodes;

    internal partial class YNodeTranslator
    {
        private YAlias GetAliasValueDependent(ITokenizer tokenizer)
        {
            if (tokenizer.Current.Value.Kind != TokenKind.Alias)
            {
                return null;
            }

            var anchorName = tokenizer.Current.Value.Value;
            if (!tokenizer.Anchors.ContainsKey(anchorName))
            {
                throw ParseException.Tokenizer(tokenizer, $"Not found anchorName: {anchorName}");
            }

            var anchorValue = tokenizer.Anchors[anchorName];
            tokenizer.MoveNext();
            return new YAlias(anchorName, anchorValue);
        }
    }
}