namespace Xamarin.Yaml.Parser.Translators
{
    using Xamarin.Yaml.Parser.Core;
    using Xamarin.Yaml.Parser.Nodes;

    internal partial class YNodeTranslator
    {
        private YAnchor GetAnchorValueDependent(ITokenizer tokenizer)
        {
            switch (tokenizer.Current.Value.Kind)
            {
                    // check "name : anchor"
                    case TokenKind.Indent when tokenizer.Current.Next?.Next?.Next?.Value.Kind == TokenKind.Anchor:
                        tokenizer.MoveNext();
                        tokenizer.MoveNext();
                        tokenizer.MoveNext();
                        var name = tokenizer.Current.Value.Value;
                        tokenizer.MoveNext();
                        var anchor = new YAnchor(this.GetNodeValue(tokenizer));
                        tokenizer.Anchors[name] = anchor;
                        return anchor;
                    default:
                        return null;
            }
        }
    }
}