using System.Collections.Generic;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Configurations {

    public class ParserContainersConfigurator {

        IParser ContainerForNonArgumentRulesParser() => new SymbolParser();

        IParser ContainerForOneRuleArgumentParser() => new ParserContainer(new List<IParser> { new OneOrZeroParser() });
    }
}
