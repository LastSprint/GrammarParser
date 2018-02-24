
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes {

    /// <summary>
    /// Объединяет несколько парсеров, у которых условия корректного парсинга схожи.
    /// </summary>
    public class ParserContainer: IParser {

        private IReadOnlyCollection<IParser> _parsers;

        public ParserContainer(IReadOnlyCollection<IParser> parsers) => this._parsers = parsers;

        public bool IsCurrentRule(IParserImmutableContext context) => this._parsers.Any(x => x.IsCurrentRule(context));

        public IRule Parse(IParserImmutableContext conext) => this._parsers.First(x => x.IsCurrentRule(conext)).Parse(conext);

    }
}
