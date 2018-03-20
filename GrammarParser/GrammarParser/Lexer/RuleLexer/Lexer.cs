using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Exceptions;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Library;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer.RuleLexer {

    /// <summary>
    ///     Объет, который умеет разбирать поток символов в поток правил.
    ///     То есть он умеет смтроить AST дерево по конкретной грамматике.
    /// </summary>
    public class Lexer : ILexer {

        private const char StartGroup = '(';

        private const char EndGroup = ')';

        private readonly IParser _parser;

        private readonly IBuilder<ILexer, Stream> _selfBuilder;

        private DefaultParserContext _context;

        public Lexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder) {
            this._selfBuilder = selfBuilder;
            this._parser = parser;
        }

        public IParserContext Parse(Stream stream) {
            this._context = new DefaultParserContext(stream) {LexerBuilder = this._selfBuilder};
            var symbol = stream.CurrentSymbol();

            while (symbol != null) {
                /**
                switch (symbol)
                {

                    case Lexer.StartGroup:

                        this._context.CurrentStream.TryToSeekToNext();
                        var context = this._selfBuilder.Build(this._context.CurrentStream);
                        var result = context.Parse(this._context.CurrentStream).ParsedRules.ToArray().Reverse();


                        if (this._context.CurrentStream.CurrentSymbol() != Lexer.EndGroup)
                        {
                            throw new LexerBadEndGroupDeclarationException(this._context);
                        }

                        this._context.CurrentStream.TryToSeekToNext();

                        var rule = new GroupRule(result.ToImmutableList());
                        this._context.ParsedRules.Push(rule);

                        break;

                    case Lexer.EndGroup:
                        return this._context;

                    default:
                        if (this._parser.IsCurrentRule(this._context))
                        {
                            this._context.ParsedRules.Push(this._parser.Parse(this._context));
                            break;
                        }

                        throw new ArgumentOutOfRangeException(
                            $"{symbol} не разобран ни одним из существующих правил.{Environment.NewLine}Контекст: {this._context}");

                }
    */
                this.ParseRule(stream);
                symbol = stream.CurrentSymbol();
                if (symbol == EndGroup) {
                    return this._context;
                }
            }

            return this._context;
        }

        public IRule ParseNextRule(Stream stream) {
            this._context = new DefaultParserContext(stream) {LexerBuilder = this._selfBuilder};
            return this.ParseRule(stream).ParsedRules.FirstOrDefault();
        }

        private IParserContext ParseRule(Stream stream) {
            var symbol = stream.CurrentSymbol();


            switch (symbol) {
                case StartGroup:

                    this._context.CurrentStream.TryToSeekToNext();
                    var context = this._selfBuilder.Build(this._context.CurrentStream);
                    var result = context.Parse(this._context.CurrentStream).ParsedRules.ToArray().Reverse();


                    if (this._context.CurrentStream.CurrentSymbol() != EndGroup) {
                        throw new LexerBadEndGroupDeclarationException(this._context);
                    }

                    this._context.CurrentStream.TryToSeekToNext();

                    var rule = new GroupRule(result.ToImmutableList());
                    this._context.ParsedRules.Push(rule);

                    break;

                case EndGroup:
                    return this._context;

                default:
                    if (this._parser.IsCurrentRule(this._context)) {
                        this._context.ParsedRules.Push(this._parser.Parse(this._context));
                        break;
                    }

                    throw new ArgumentOutOfRangeException(
                        $"{symbol} не разобран ни одним из существующих правил.{Environment.NewLine}Контекст: {this._context}");
            }

            return this._context;
        }

    }

}