using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Library;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer {
    
    /// <summary>
    /// Объет, который умеет разбирать поток символов в поток правил.
    /// То есть он умеет смтроить AST дерево по конкретной грамматике.
    /// </summary>
    public class Lexer: ILexer {

        private const char StartGroup = '(';
        private const char EndGroup = ')';

        private readonly IParser _parser;

        private readonly IBuilder<ILexer, Stream> _selfBuilder;

        private DefaultParserContext _context;

        public Lexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder) {
            this._parser = parser;
            this._selfBuilder = selfBuilder;
        }

        public IParserContext Parse(Stream stream) {
            //TODO: Убрать рекурсию. Серьезно. 
            this._context = new DefaultParserContext(stream: stream);
            var symbol = stream.CurrentSymbol();
            while (symbol != null) {
                switch (symbol) {

                    case Lexer.EndGroup:
                        return this._context;
                    case null:
                        return this._context;

                    case Lexer.StartGroup:
                        var context = this._selfBuilder.Build(stream);
                        var result = context.Parse(stream).ParsedRules.ToArray().Reverse();
                        var rule = new GroupRule(result.ToImmutableList());
                        this._context.ParsedRules.Push(rule);
                        break;
                    default:
                        if (this._parser.IsCurrentRule(this._context)) {
                            this._context.ParsedRules.Push(this._parser.Parse(this._context));
                            break;
                        }
                        
                        throw new ArgumentOutOfRangeException($"{symbol} не разобран ни одним из существующих правил.{Environment.NewLine}Контекст: {this._context}");
                }
                symbol = stream.CurrentSymbol();
            }

            return this._context;
        }
    }
}
