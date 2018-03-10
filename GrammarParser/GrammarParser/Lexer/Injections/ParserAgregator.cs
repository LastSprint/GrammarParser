using System.Collections.Generic;
using System.Linq;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Injections {

    /// <summary>
    /// Содержит внутри себя другие парсеры и инкапсулирует логику их вызова
    /// </summary>
    public class ParserAgregator: IParser {

        private readonly IReadOnlyList<IParser> _parsers;

        public ParserAgregator(IReadOnlyList<IParser> parsers) {
            this._parsers = parsers;
        }

        public bool IsCurrentRule(IParserImmutableContext context) {
            return this._parsers.Any(x => x.IsCurrentRule(context));
        }

        public IRule Parse(IParserImmutableContext conext) {
            return this._parsers.First(x => x.IsCurrentRule(conext))?.Parse(conext);
        }
    }
}