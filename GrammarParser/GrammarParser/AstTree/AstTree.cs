using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree.Interfaces;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library.Extensions;

namespace GrammarParser.AstTree {

    public class AstTree: IAstTree {

        public IAstNode Root { get; private set; }

        public int NodeCount { get; private set; }

        public IList<IAstNode> Childs => this.Root.Childs;

        public AstTree(IParserImmutableContext context) {
            this.NodeCount = 0;
            this.InitializeWithContext(context);
        }

        public bool Check(Stream stream) => this.Root.Check(stream) && stream.CurrentSymbol() == null;

        private void InitializeWithContext(IParserImmutableContext context) {

            IAstNode currentNode = null;

            foreach (var rule in context.CurrentRuleCollection) {
                var temp = new AstNode(rule);
                this.NodeCount += 1;
                if (currentNode != null) {
                    temp.Childs.Add(currentNode);
                }
                
                currentNode = temp;
            }

            this.Root = currentNode;
        }
    }
}
