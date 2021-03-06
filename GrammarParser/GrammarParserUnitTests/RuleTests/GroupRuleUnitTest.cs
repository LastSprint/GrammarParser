﻿using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.RuleTests {

    [TestClass]
    public class GroupRuleUnitTest {

        [TestMethod]
        public void TestRulePriority() {

            // arrage

            var symbol = 'a';

            // act

            var rule = new GroupRule(new List<IRule> {new SymbolRule(symbol: symbol)});

            // assert

            Assert.AreEqual(rule.Priority, RulePriority.RuleGrouping);
        }

        [TestMethod]
        public void TestFailChekingOneSymbol() {
            //arrange

            var symbol = 'd';
            var rule = new GroupRule(new List<IRule> { new SymbolRule(symbol: symbol) });
            var stream = new MemoryStream().FromString((symbol + 1).ToString());

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsFalse(isCheckedSuccess);
        }

        [TestMethod]
        public void TestStreamPositionOffsetCorrectlyWithCorrectSymbol()
        {

            // Arrange

            var symbol = 'e';
            var rule = new GroupRule(new List<IRule> { new SymbolRule(symbol: symbol) });
            var stream = new MemoryStream().FromString("example test string that so biger then needed");
            var startPosition = stream.Position;

            // Act

            rule.Check(stream);

            // Assert

            Assert.AreEqual(stream.Position - 1, startPosition);
        }


        [TestMethod]
        public void TestStreamPositionOffsetCorrectlyWithIncorrectSymbol()
        {

            // Arrange

            var symbol = 'g';
            var rule = new GroupRule(new List<IRule> { new SymbolRule(symbol: symbol) });

            var stream = new MemoryStream().FromString("example test string that so biger then needed");
            var startPosition = stream.Position;

            // Act

            rule.Check(stream);

            // Assert

            Assert.AreEqual(stream.Position, startPosition);
        }


        [TestMethod]
        public void TestManySuccessivelyChekingWithLastFailed() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';
            var symbol3 = 'i';

            var rule1 = new SymbolRule(symbol: symbol1);
            var rule2 = new SymbolRule(symbol: symbol2);
            var rule3 = new SymbolRule(symbol: symbol3);

            var rule = new GroupRule(new List<IRule> { rule1, rule2, rule3 });


            var stream = new MemoryStream().FromString($"{symbol1}{symbol2}/{symbol3}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsFalse(result, message: "result is false");
        }


        [TestMethod]
        public void TestManySuccessivelyCheking(){

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';
            var symbol3 = 'i';

            var rule1 = new SymbolRule(symbol: symbol1);
            var rule2 = new SymbolRule(symbol: symbol2);
            var rule3 = new SymbolRule(symbol: symbol3);

            var rule = new GroupRule(new List<IRule> { rule1, rule2, rule3 });


            var stream = new MemoryStream().FromString($"{symbol1}{symbol2}{symbol3}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleSuccessWithFullString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2});


            var stream = new MemoryStream().FromString($"{symbol1}{symbol2}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleSuccessWitoutLastComponentString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });


            var stream = new MemoryStream().FromString($"{symbol1}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleSuccessWitoutLastComponentAndWithManyFirstComponentString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });


            var stream = new MemoryStream().FromString($"{symbol1}{symbol1}{symbol1}{symbol1}{symbol1}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleSuccessWithBigString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });


            var stream = new MemoryStream().FromString($"{symbol1}{symbol1}{symbol1}{symbol1}{symbol1}{symbol2}{symbol2}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleFailedWithoutSecondComponentString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });


            var stream = new MemoryStream().FromString($"{symbol1}{symbol1}{symbol1}{symbol1}{symbol1}w");


            // Act

            var result = rule.Check(stream);

            // Assert

            Assert.IsTrue(result, message: "result is false");
        }

        [TestMethod]
        public void TestHardRuleFailedWithoutFirstComponentString() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });


            var stream = new MemoryStream().FromString($"${symbol2}");


            // Act

            var result = rule.Check(stream);


            // Assert

            Assert.IsFalse(result, message: "result is false");
        }

        [TestMethod]
        public void TestRighSringSavedInSuccessCase() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });

            var str = $"{symbol1}{symbol1}{symbol1}{symbol1}{symbol1}{symbol2}{symbol2}";
            var stream = new MemoryStream().FromString(str);


            // Act

            var result = rule.Check(stream);

            // Assert

            Assert.AreEqual(str, rule.ChekedString);
        }

        [TestMethod]
        public void TestRighSringSavedInFaledCase() {
            
            var symbol1 = 'g';
            var symbol2 = 'b';

            var rule11 = new SymbolRule(symbol: symbol1);
            var rule1 = new OneOrManyRule(rule11);
            var rule22 = new SymbolRule(symbol: symbol2);
            var rule2 = new ZeroOrManyRule(rule22);
            var rule = new GroupRule(new List<IRule> { rule1, rule2 });

            var str = $"${symbol2}";
            var stream = new MemoryStream().FromString(str);

            // Act
            rule.Check(stream);

            // Assert

            Assert.AreEqual(string.Empty, rule.ChekedString);
        }

    }
}

