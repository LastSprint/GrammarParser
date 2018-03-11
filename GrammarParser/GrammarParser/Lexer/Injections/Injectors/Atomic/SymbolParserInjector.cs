using System.Collections.Generic;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Injections.Injectors.Atomic {

    /// <summary>
    /// Инъектирует парсер для символов.
    /// <see cref="SymbolParser"/>
    /// </summary>
    public class SymbolParserInjector: IInjector<IParser> {
        public IParser Injection() => new ParserAgregator(new List<IParser> { new SymbolParser() });
    }
}
