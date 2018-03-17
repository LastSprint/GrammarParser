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
    /// Объет, который умеет разбирать поток символов в поток правил.
    /// То есть он умеет смтроить AST дерево по конкретной грамматике.
    /// </summary>
    public class Lexer: SingleRuleLexer {

        private DefaultParserContext _context;

        public Lexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder): base(parser, selfBuilder) { }

        public new IParserContext Parse(Stream stream) {
            this._context = new DefaultParserContext(stream: stream);
            var symbol = stream.CurrentSymbol();
            while (symbol != null) {
                base.ParseCurrentSymbol(symbol)
                    .ParsedRules.Reverse()
                    .ToList()
                    .ForEach(x => this._context.ParsedRules.Push(x));
                symbol = stream.CurrentSymbol();
            }
            return this._context;
        }
    }
}