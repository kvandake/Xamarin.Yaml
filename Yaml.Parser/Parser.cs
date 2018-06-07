namespace Xamarin.Yaml.Parser
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Xamarin.Yaml.Parser.Attributes;
    using Xamarin.Yaml.Parser.Core;
    using Xamarin.Yaml.Parser.Exceptions;
    using Xamarin.Yaml.Parser.Nodes;
    using Xamarin.Yaml.Parser.Translators;

    [Preserve(AllMembers = true)]
    public class Parser
    {
        private readonly ParserConfig config;

        public Parser(ParserConfig config, params string[] contents)
        {
            this.config = config;
            this.map = this.ParseContent(contents);
        }

        public bool ThrowWhenKeyNotFound => this.config.ThrowWhenKeyNotFound;

        public string Separator => this.config.Separator;

        private IDictionary<string, string> map { get; }

        public IDictionary<string, string> Dump()
        {
            return this.map.ToDictionary(x => x.Key, y => y.Value);
        }

        public string FindValue(params string[] innerKeys)
        {
            if (innerKeys.Length == 0)
            {
                return string.Empty;
            }

            var key = string.Join(this.Separator, innerKeys);
            return this.FindValue(key);
        }

        public string FindValue(string key)
        {
            if (this.map.ContainsKey(key))
            {
                return this.map[key];
            }

            return this.ThrowWhenKeyNotFound
                ? throw new KeyNotFoundException($"Key <{key}> not found in the current locale")
                : string.Empty;
        }

        private IDictionary<string, string> ParseContent(string content)
        {
            var dict = new Dictionary<string, string>();
            var nodes = GetNodes(content);
            foreach (var node in nodes)
            {
                this.TryParseContentItems(node, ref dict);
            }

            return new ConcurrentDictionary<string, string>(dict);
        }

        private IDictionary<string, string> ParseContent(string[] contents)
        {
            var dict = new Dictionary<string, string>();
            foreach (var content in contents)
            {
                foreach (var dictItem in this.ParseContent(content))
                {
                    dict[dictItem.Key] = dictItem.Value;
                }
            }

            return new ConcurrentDictionary<string, string>(dict);
        }

        private void TryParseContentItems(YNode node, ref Dictionary<string, string> dict, string prefix = null)
        {
            switch (node)
            {
                case YKeyValuePair keyValuePair:
                    string key = null;
                    try
                    {
                        key = (string) keyValuePair.Key;
                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    key = string.IsNullOrEmpty(prefix) ? key : string.Concat(prefix, this.Separator, key);
                    if (keyValuePair.Value is YScalar valueScalar)
                    {
                        dict[key] = valueScalar.Value;
                    }
                    else
                    {
                        this.TryParseContentItems(keyValuePair.Value, ref dict, key);
                    }

                    break;
                case YSequence sequence:
                    // TODO: need suppoer Enum fields
                    foreach (var sequenceChild in sequence.Children)
                    {
                        if (sequenceChild is YScalar yScalar)
                        {
                            dict[string.Concat(prefix, this.Separator, yScalar.Value)] = yScalar.Value;
                        }
                    }

                    break;
                case YMapping mapping:
                    foreach (var mapItem in mapping)
                    {
                        this.TryParseContentItems(mapItem, ref dict, prefix);
                    }

                    break;

                case IEnumerable<YNode> collection:
                    foreach (var colItem in collection)
                    {
                        this.TryParseContentItems(colItem, ref dict, prefix);
                    }

                    break;
            }
        }

        private static IEnumerable<YNode> GetNodes(string content)
        {
            using (var scanner = new Scanner(content))
            {
                using (var tokenizer = new Tokenizer(scanner))
                {
                    var nodeTranslator = new YNodeTranslator();
                    while (tokenizer.Current.Value.Kind != TokenKind.Eof)
                    {
                        var node = nodeTranslator.Translate(tokenizer);
                        if (node is YAnchor)
                        {
                            continue;
                        }

                        yield return node;
                    }

                    while (tokenizer.Current.Value.Kind == TokenKind.Unindent)
                    {
                        tokenizer.MoveNext();
                    }

                    if (tokenizer.Current.Value.Kind != TokenKind.Eof)
                    {
                        throw ParseException.UnexpectedToken(tokenizer, TokenKind.Eof);
                    }
                }
            }
        }
    }
}