using System.Collections.Generic;

using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.RuleLexer.Injections.Injectors.Atomic {

    /// <summary>
    ///     Инъектирует все парсеры, содержащие два аргумент.
    ///     <see cref="RangeRuleParser" />
    ///     <see cref="DisjunctionRuleParser" />
    /// </summary>
    public class TwoArgumentParserIinjector : IInjector<IParser> {

        public IParser Injection() =>
            new ParserAgregator(new List<IParser> {new RangeRuleParser(), new DisjunctionRuleParser()});

    }

}