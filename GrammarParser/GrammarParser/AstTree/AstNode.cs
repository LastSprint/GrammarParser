using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.AstTree {

    public class AstNode: IAstNode {

        private readonly IRule _rule;

        public IList<IAstNode> Childs { get; }

        public AstNode(IRule rule) {
            this.Childs = new List<IAstNode>();
            this._rule = rule;
        }

        public bool Check(Stream stream) => this._rule.Check(stream) && this.Childs.All(x => x.Check(stream));
    }
}
