using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Parser.Classes {

    public class DefaultParserContext : IParserContext {

        private static DefaultParserContext _globalContext; 

        //  FIXIT: - Make session parsing
        public static DefaultParserContext GlobalContext {
            get {
                if (_globalContext == null) {
                    _globalContext = new DefaultParserContext(null);
                }

                return _globalContext;
            }
        }

        public DefaultParserContext(Stream stream) {
            this.CurrentStream = stream;
            this.ParsedRules = new Stack<IRule>();
        }

        public IBuilder<ILexer, Stream> LexerBuilder { get; set; }

        public IReadOnlyCollection<IRule> CurrentRuleCollection => this.ParsedRules;
        public IReadOnlyCollection<UserRule> UserRules => this.CurrentRuleCollection.OfType<UserRule>().ToList();

        public Stack<IRule> ParsedRules { get; set; }

        public IRule Pop() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Pop();

        public IRule Peek() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Peek();

        public Stream CurrentStream { get; }

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