// GrammarParserUnitTests
// SymbolRuleUnitTest.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.Types.Other;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using GrammarParserUnitTests.Utils;

namespace GrammarParserUnitTests.RuleTests {

    [TestClass]
    public class SymbolRuleUnitTest {

        [TestMethod]
        public void TestRulePriority() {
            
            // arrage

            var symbol = 'a';

            // act

            var rule = new SymbolRule(symbol: symbol);

            // assert

            Assert.AreEqual(rule.Priority, RulePriority.RuleSymbol);
        }

        [TestMethod]
        public void TestFailChekingOneSymbol() {
            //arrange

            var symbol = 'd';
            var rule = new SymbolRule(symbol: symbol);
            var stream = new MemoryStream().FromChar((char)(symbol + 1));

            //act

            var isCheckedSuccess = rule.Check(stream);

            //assert

            Assert.IsFalse(isCheckedSuccess);
        }

        [TestMethod]
        public void TestStreamPositionOffsetCorrectlyWithCorrectSymbol() {

            // Arrange

            var symbol = 'e';
            var rule = new SymbolRule(symbol: symbol);
            var stream = new MemoryStream().FromString("example test string that so biger then needed");
            var startPosition = stream.Position;

            // Act

            rule.Check(stream);

            // Assert

            Assert.AreEqual(stream.Position - 1, startPosition);
        }


        [TestMethod]
        public void TestStreamPositionOffsetCorrectlyWithIncorrectSymbol() {

            // Arrange

            var symbol = 'g';
            var rule = new SymbolRule(symbol: symbol);
            var stream = new MemoryStream().FromString("example test string that so biger then needed");
            var startPosition = stream.Position;

            // Act

            rule.Check(stream);

            // Assert

            Assert.AreEqual(stream.Position, startPosition);
        }


        [TestMethod]
        public void TestManySuccessivelyChekingl() {

            // Arrange

            var symbol1 = 'g';
            var symbol2 = 'b';
            var symbol3 = 'i';

            var rule1 = new SymbolRule(symbol: symbol1);
            var rule2 = new SymbolRule(symbol: symbol2);
            var rule3 = new SymbolRule(symbol: symbol3);

            var stream = new MemoryStream().FromString($"{symbol1}{symbol2}/{symbol3}");


            // Act

            var rule1Result = rule1.Check(stream);
            var rule2Result = rule2.Check(stream);
            var rule3Result = rule3.Check(stream);


            // Assert

            Assert.IsTrue(rule1Result, message: "rule1Result is false");
            Assert.IsTrue(rule2Result, message: "rule2Result is false");
            Assert.IsFalse(rule3Result, message: "rule4Result is false");
        }

        [TestMethod]
        public void TestSuccessChekingSpecialSymbol() {

            // Arrange

            var symbol = '\r';
            var rule = new SymbolRule(symbol: symbol);
            var stream = new MemoryStream().FromString("\rtnmp");

            // Act
            var startPos = stream.Position;
            rule.Check(stream);

            // Assert

            Assert.AreEqual(startPos + 1, stream.Position);
        }
    }
}
