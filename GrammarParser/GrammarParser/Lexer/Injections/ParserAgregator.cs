using System.Collections.Generic;
using System.Linq;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Injections {

    /// <summary>
    /// Содержит внутри себя другие парсеры и инкапсулирует логику их вызова
    /// </summary>
    public class ParserAgregator: IParser {

        public readonly IReadOnlyList<IParser> Parsers;

        public ParserAgregator(IReadOnlyList<IParser> parsers) => this.Parsers = parsers;

        public bool IsCurrentRule(IParserImmutableContext context) {
            return this.Parsers.Any(x => x.IsCurrentRule(context));
        }

        public IRule Parse(IParserImmutableContext conext) {
            return this.Parsers.First(x => x.IsCurrentRule(conext))?.Parse(conext);
        }
    }
}