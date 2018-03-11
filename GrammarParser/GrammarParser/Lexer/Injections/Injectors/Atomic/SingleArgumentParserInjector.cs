using System.Collections.Generic;

using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Injections.Injectors.Atomic {

    /// <summary>
    /// Инъектирует все парсеры, содержащие один аргумент.
    /// <see cref="OneOrManyParser"/>
    /// <see cref="OneOrZeroParser"/>
    /// <see cref="ZeroOrManyParser"/>
    /// </summary>
    public class SingleArgumentParserInjector: IInjector<IParser> {
        public IParser Injection() => new ParserAgregator(new List<IParser> { new OneOrManyParser(), new OneOrZeroParser(), new ZeroOrManyParser()});
    }
}
