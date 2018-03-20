using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Library.Extensions;

namespace GrammarParser.AstTree {

    public class AstNode: IAstNode {

        private readonly IRule _rule;

        public IList<IAstNode> Childs { get; }

        public string ParsedResult { get; private set; }

        public AstNode(IRule rule) {
            this.Childs = new List<IAstNode>();
            this._rule = rule;
        }

        public bool Check(Stream stream) {

            var checkResult = this._rule.Check(stream);

            if (checkResult) {

                this.ParsedResult = this._rule.ChekedString;
            }

            return checkResult && this.Childs.All(x => x.Check(stream));
        }

    }
}
