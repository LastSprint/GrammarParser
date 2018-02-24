using System;
using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes
{

    public class DefaultParserContext: IParserContext {

        public IReadOnlyCollection<IRule> CurrentRuleCollection => this.ParsedRules;
        public Stack<IRule> ParsedRules { get; set; }

        public Stream CurrentStream { get; }

        public DefaultParserContext(Stream stream) {
            this.CurrentStream = stream;
            this.ParsedRules = new Stack<IRule>();
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
