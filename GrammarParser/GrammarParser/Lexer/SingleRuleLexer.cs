using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using GrammarParser.Lexer.Exceptions;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Library;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer {

    /// <summary>
    /// Лексер, который умеет просто доставать одно правило из потока
    /// </summary>
    public class SingleRuleLexer : ILexer {

        protected const char StartGroup = '(';
        protected const char EndGroup = ')';

        protected readonly IParser Parser;

        protected readonly IBuilder<ILexer, Stream> SelfBuilder;

        protected DefaultParserContext Context;

        public SingleRuleLexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder) {
            this.Parser = parser;
            this.SelfBuilder = selfBuilder;
        }

        public virtual IParserContext Parse(Stream stream) {
            this.Context = new DefaultParserContext(stream: stream);
            var symbol = stream.CurrentSymbol();
            return this.ParseCurrentSymbol(symbol);
        }

        protected IParserContext ParseCurrentSymbol(char? symbol) {

            switch (symbol) {
               
                case null:
                    return this.Context;

                default:

                    if (this.Parser.IsCurrentRule(this.Context)) {
                        this.Context.ParsedRules.Push(this.Parser.Parse(this.Context));
                        return this.Context;
                    }

                    throw new ArgumentOutOfRangeException(
                        $"{symbol} не разобран ни одним из существующих правил.{Environment.NewLine}Контекст: {this.Context}");
            }
        }
    }
}
