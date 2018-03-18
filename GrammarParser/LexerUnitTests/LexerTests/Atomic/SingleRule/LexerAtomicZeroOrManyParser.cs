using System;
using System.Collections.Generic;

using GrammarParser.Lexer;
using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Library;
using GrammarParser.RuleLexer;
using GrammarParser.RuleLexer.Configurations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests.LexerTests.Atomic.SingleRule {

    [TestClass]
    public class LexerAtomicZeroOrManyParser: LexerAtomicSingleRuleParser {
        public class TestEnjector : IInjector<IParser> {
            public IParser Injection() => new ParserAgregator(new List<IParser> { new SymbolParser(), new ZeroOrManyParser() });
        }

        public override ILexer Lexer => new Lexer(new TestEnjector().Injection(),
            new LexerBuilder(new TestEnjector()));

        public override char Symbol => ZeroOrManyParser.Symbol;

        public override Type CurrentRuleType => typeof(ZeroOrManyRule);
    }
}
