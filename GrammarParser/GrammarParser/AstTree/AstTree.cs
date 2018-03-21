using System;
using System.Collections.Generic;
using System.IO;

using GrammarParser.AstTree.Interfaces;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library.Extensions;

namespace GrammarParser.AstTree {

    public class AstTree: IAstTree {

        public IAstNode Root { get; private set; }

        public int NodeCount { get; private set; }

        public IList<IAstNode> Childs => this.Root.Childs;
         
        public string ParsedResult => this.Root?.ParsedResult;

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

        /// <summary>
        /// Производит обход в глубину по дереву, если корень дерева null, то обход не осуществляется
        /// </summary>
        /// <param name="action">Получает каждый сдежующий узел дерева</param>
        public void DeepWalk(Action<IAstNode> action) {

            if (this.Root == null) {
                return;
            }

            this.ReqursiveDeepWalk(action, this.Root);
        }

        private void ReqursiveDeepWalk(Action<IAstNode> action, IAstNode node) {
            action(node);

            foreach (var nodeChild in node.Childs) {
                this.ReqursiveDeepWalk(action, nodeChild);
            }
        }

    }
}
