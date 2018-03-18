using System.Collections.Generic;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Library;
using GrammarParser.RuleLexer.Injections.Injectors.Atomic;

namespace GrammarParser.Lexer.Injections.Injectors {

    /// <summary>
    ///     Агрегирует все простые парсеры:
    ///     <see cref="SingleArgumentParserInjector" />
    ///     <see cref="SymbolParserInjector" />
    ///     <see cref="TwoArgumentParserIinjector" />
    /// </summary>
    public class SimpleParserInjector : IInjector<IParser> {

        public IParser Injection() => new ParserAgregator(new List<IParser> {
            new SingleArgumentParserInjector().Injection(),
            new SymbolParserInjector().Injection(),
            new TwoArgumentParserIinjector().Injection()
        });

    }

}