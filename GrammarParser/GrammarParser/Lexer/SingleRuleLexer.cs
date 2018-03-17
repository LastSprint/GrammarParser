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

        protected readonly IParser _parser;

        protected readonly IBuilder<ILexer, Stream> _selfBuilder;

        private DefaultParserContext _context;

        public SingleRuleLexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder) {
            this._parser = parser;
            this._selfBuilder = selfBuilder;
        }

        public IParserContext Parse(Stream stream) {
            this._context = new DefaultParserContext(stream: stream);
            var symbol = stream.CurrentSymbol();
            return this.ParseCurrentSymbol(symbol);
        }

        protected IParserContext ParseCurrentSymbol(char? symbol) {

            switch (symbol) {
                case SingleRuleLexer.EndGroup:
                    return this._context;
                case null:
                    return this._context;
                case SingleRuleLexer.StartGroup:
                    this._context.CurrentStream.TryToSeekToNext();
                    var context = this._selfBuilder.Build(this._context.CurrentStream);
                    var result = context.Parse(this._context.CurrentStream).ParsedRules.ToArray().Reverse();

                    if (this._context.CurrentStream.CurrentSymbol() != SingleRuleLexer.EndGroup) {
                        throw new LexerBadGroupDeclarationException(this._context);
                    }

                    this._context.CurrentStream.TryToSeekToNext();

                    var rule = new GroupRule(result.ToImmutableList());
                    this._context.ParsedRules.Push(rule);

                    return this._context;

                default:

                    if (this._parser.IsCurrentRule(this._context)) {
                        this._context.ParsedRules.Push(this._parser.Parse(this._context));
                        return this._context;
                    }

                    throw new ArgumentOutOfRangeException(
                        $"{symbol} не разобран ни одним из существующих правил.{Environment.NewLine}Контекст: {this._context}");
            }
        }
    }
}
