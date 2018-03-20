using System;
using System.Collections.Generic;

using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Configurations;
using GrammarParser.Lexer.RuleLexer.Parser.Classes;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Library;
using GrammarParser.RuleLexer;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests.LexerTests.Atomic.SingleRule {

    [TestClass]
    public class LexerAtomicOneOrZeroParserTests: LexerAtomicSingleRuleParser{

        public class TestEnjector: IInjector<IParser> {
            public IParser Injection() => new ParserAgregator(new List<IParser> { new SymbolParser(), new OneOrZeroParser() });
        }

        public override ILexer Lexer => new Lexer(new TestEnjector().Injection(),
           new LexerBuilder(new TestEnjector()));

        public override char Symbol => OneOrZeroParser.Symbol;

        public override Type CurrentRuleType => typeof(OneOrZeroRule);
    }
}
