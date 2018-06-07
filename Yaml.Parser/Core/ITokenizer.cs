namespace Xamarin.Yaml.Parser.Core
{
    using System.Collections.Generic;
    using Xamarin.Yaml.Parser.Nodes;

    internal interface ITokenizer
    {
        IDictionary<string, YAnchor> Anchors { get; }
        
        LinkedList<Token> Tokens { get; }

        LinkedListNode<Token> Current { get; }

        bool MoveNext();
        
        Scanner Scanner { get; }
    }
}