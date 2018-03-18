using System.Collections.Generic;

using GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Injections.Injectors.Atomic {

    /// <summary>
    /// Инъектирует все парсеры, содержащие два аргумент.
    /// <see cref="RangeRuleParser"/>
    /// </summary>
    public class TwoArgumentParserIinjector: IInjector<IParser> {

        public IParser Injection() => new ParserAgregator(new List<IParser> { new RangeRuleParser() });
    }
}
