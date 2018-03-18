using System.Collections.Generic;
using System.Linq;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes {

    /// <summary>
    ///     Объединяет несколько парсеров, у которых условия корректного парсинга схожи.
    /// </summary>
    public class ParserContainer : IParser {

        private readonly IReadOnlyCollection<IParser> _parsers;

        public ParserContainer(IReadOnlyCollection<IParser> parsers) => this._parsers = parsers;

        public bool IsCurrentRule(IParserImmutableContext context) => this._parsers.Any(x => x.IsCurrentRule(context));

        public IRule Parse(IParserImmutableContext conext) =>
            this._parsers.First(x => x.IsCurrentRule(conext)).Parse(conext);

    }

}