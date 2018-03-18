using System.Collections.Generic;

using GrammarParser.Lexer.Injections.Injectors.Atomic;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Injections.Injectors {

    /// <summary>
    /// Агрегирует все простые парсеры:
    /// <see cref="SingleArgumentParserInjector"/>
    /// <see cref="SymbolParserInjector"/>
    /// <see cref="TwoArgumentParserIinjector"/>
    /// </summary>
    public class SimpleParserInjector: IInjector<IParser> {

        public IParser Injection() => new ParserAgregator( new List<IParser> {
            new SingleArgumentParserInjector().Injection(),
            new SymbolParserInjector().Injection(),
            new TwoArgumentParserIinjector().Injection() 
        });
    }
}
