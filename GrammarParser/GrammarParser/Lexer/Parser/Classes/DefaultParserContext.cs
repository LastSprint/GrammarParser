using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes
{

    public class DefaultParserContext: IParserContext {

        public IImmutableQueue<IRule> CurrentRuleCollection => ImmutableQueue.CreateRange(this.ParsedRules);
        public Queue<IRule> ParsedRules { get; set; }

        public Stream CurrentStream { get; }

        public DefaultParserContext(Stream stream) {
            this.CurrentStream = stream;
            this.ParsedRules = new Queue<IRule>();
        }
    }
}
