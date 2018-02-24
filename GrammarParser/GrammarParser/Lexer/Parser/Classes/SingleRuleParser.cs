
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes {

    /// <summary>
    /// Парсер для разбора правил с одним аргументом (сдева от terminate-символа)
    /// </summary>
    public class SingleRuleParser: IParser {

        private IReadOnlyCollection<char> _operationSymbols = new List<char> { '?', '+', '*' };

        public bool IsCurrentRule(IParserImmutableContext context) {
            throw new System.NotImplementedException();
        }

        public IRule Parse(IParserImmutableContext conext) {

            var stream = conext.CurrentStream;
            var lastParsedRule = conext.CurrentRuleCollection.First();
            throw new System.NotImplementedException();
        }

    }
}
