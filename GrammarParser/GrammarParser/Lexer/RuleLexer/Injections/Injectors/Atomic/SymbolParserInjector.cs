using System.Collections.Generic;

using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Classes;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.RuleLexer.Injections.Injectors.Atomic {

    /// <summary>
    ///     Инъектирует парсер для символов.
    ///     <see cref="SymbolParser" />
    /// </summary>
    public class SymbolParserInjector : IInjector<IParser> {

        public IParser Injection() => new ParserAgregator(new List<IParser> {new SymbolParser()});

    }

}