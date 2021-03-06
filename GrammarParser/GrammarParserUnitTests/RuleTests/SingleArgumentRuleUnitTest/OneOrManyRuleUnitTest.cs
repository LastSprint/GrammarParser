﻿using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.RuleTests.SingleArgumentRuleUnitTest {

    [TestClass]
    public class OneOrManyRuleUnitTest {

        [TestMethod]
        public void TestRulePriority() {
            var argumentRule = new SymbolRule(symbol: 'b');


            //act

            var rule = new OneOrManyRule(argument: argumentRule);

            //assert

            Assert.AreEqual(rule.Priority, RulePriority.RuleOneOrMany);
        }

        [TestMethod]
        public void TestSuccessChekingOneSymbolWithFailArgumentRule() {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString((symbol + 1).ToString());

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsFalse(isCheckedSuccess);
        }

        [TestMethod]
        public void TestThatChekingSuccessWithTwoSameSymbols() {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString($"{symbol}{symbol}");

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsTrue(isCheckedSuccess);
        }

        [TestMethod]
        public void TestSuccessChekingOneSymbolWithSuccessArgumentRule()
        {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString(symbol.ToString());

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsTrue(isCheckedSuccess);
        }

        [TestMethod]
        public void TestSuccessChekingManySymbolWithSuccessArgumentRule()
        {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString($"{symbol}ghsdghfsjfdjhsgdfh");

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsTrue(isCheckedSuccess);
        }

        [TestMethod]
        public void TestFailedChekingManySymbolWithFailArgumentRule()
        {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString($"ghsdghfsjfdjhsgdfh");

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsFalse(isCheckedSuccess);
        }

        [TestMethod]
        public void TestSuccessPositionWithFailArgumentRule()
        {
            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString($"ghsdghfsjfdjhsgdfh");

            //act

            var startPos = stream.Position;
            var result = rule.Check(stream);
            var endPos = stream.Position;

            //assert

            Assert.IsFalse(result);
            Assert.AreEqual(startPos, endPos);
        }

        [TestMethod]
        public void TestSuccessPositionWithSuccessArgumentRule() {

            //arrange

            var symbol = 'd';
            var argumentRule = new SymbolRule(symbol: symbol);
            var rule = new OneOrManyRule(argument: argumentRule);
            var stream = new MemoryStream().FromString($"{symbol}{symbol}{symbol}{symbol}{symbol}{symbol}{symbol}{symbol}ghsdghfsjfdjhsgdfh");

            //act
            var startPos = stream.Position;
            rule.Check(stream);
            var endPos = stream.Position;

            //assert

            Assert.AreEqual(startPos + 8, endPos);
        }

        [TestMethod]
        public void TestManyRuleSuccessAll() {
            //arrange

            var symbol1 = 'd';
            var symbol2 = 'g';
            var argumentRule1 = new SymbolRule(symbol: symbol1);
            var argumentRule2 = new SymbolRule(symbol: symbol2);

            var rule1 = new OneOrManyRule(argument: argumentRule1);
            var rule2 = new OneOrManyRule(argument: argumentRule2);

            var stream = new MemoryStream().FromString($"{symbol1}{symbol1}{symbol2}jahsdkjha");

            //act

            var result = new List<bool> {
                rule1.Check(stream),
                rule2.Check(stream)
            };


            //assert

            result.ForEach(Assert.IsTrue);
        }

    }
}
