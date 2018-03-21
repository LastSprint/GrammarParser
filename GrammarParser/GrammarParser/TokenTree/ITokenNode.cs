using System.Collections.Generic;

namespace GrammarParser.TokenTree {

    public interface ITokenNode {
        string Name { get; }
        string Value { get; }

        IList<ITokenNode> Childs { get; }
    }
}
