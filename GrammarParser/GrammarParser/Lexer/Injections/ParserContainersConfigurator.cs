using System;
using System.Collections.Generic;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Injections {

    public class DefaultParserProvider: IParserProvider {
        
        public IParser NonArgumentParser => new SymbolParser();
        public IParser OneArgumentParer => new ParserContainer(new List<IParser> { new OneOrZeroParser() });
        public IParser TwoArgumentParser => throw new NotImplementedException();
    }
}
