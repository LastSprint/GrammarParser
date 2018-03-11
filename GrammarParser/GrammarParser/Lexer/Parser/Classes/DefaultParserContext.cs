using System;
using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes {

    public class DefaultParserContext: IParserContext {

        public IReadOnlyCollection<IRule> CurrentRuleCollection => this.ParsedRules;
        public Stack<IRule> ParsedRules { get; set; }

        public IRule Pop() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Pop();

        public IRule Peek() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Peek();

        public Stream CurrentStream { get; }

        public DefaultParserContext(Stream stream) {
            this.CurrentStream = stream;
            this.ParsedRules = new Stack<IRule>();
        }

        public void Merge(IParserContext context) {
            this.CurrentStream.Position = context.CurrentStream.Position;
            
        }

        public override string ToString() {
            var result = $"Stream position: {this.CurrentStream.Position}";
            result += $"{Environment.NewLine}Context:";
            foreach (var parsedRule in this.ParsedRules) {
                result += $"{Environment.NewLine}{parsedRule}";
            }
            return result;
        }
    }
}
