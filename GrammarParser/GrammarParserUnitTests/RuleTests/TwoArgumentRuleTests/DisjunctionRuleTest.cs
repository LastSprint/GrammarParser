using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Library.Extensions;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.RuleTests.TwoArgumentRuleTests {

    [TestClass]
    public class DisjunctionRuleTest {

        [TestMethod]
        public void TestRulePriority() {

            var rule = new DisjunctionRule(null, null);

            Assert.AreEqual(rule.Priority, RulePriority.RuleOr);
        }

        [TestMethod]
        public void TestChekingSucessInLeftTrueCase() {
            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString(symbol1.ToString());

            // Act

            var isCheked =
                new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestChekingSucessInRightTrueCase()
        {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString(symbol2.ToString());

            // Act

            var isCheked =
                new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestChekingFail()
        {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString((symbol2 + 1).ToString());

            // Act

            var isCheked =
                new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsFalse(isCheked);
        }



        [TestMethod]
        public void TestChekingSucessInCaseOfSameSymbols()
        {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'a';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("a");

            // Act

            var isCheked =
                new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestThatStreamCursorHasRightPositionAfterSuccessParse()
        {
            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';
            var addition = 'e';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString($"{symbol1}{addition}");

            // Act

            var streamStartPosition = stream.Position;

            new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.AreEqual(addition, stream.CurrentSymbol());
            Assert.AreEqual(streamStartPosition + 1, stream.Position);
        }

        [TestMethod]
        public void TestThatStreamCursorHasRightPositionAfterFalidParse()
        {
            // Arrange

            var symbol1 = 'c';
            var symbol2 = 'a';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("b");

            // Act

            var streamStartPosition = stream.Position;

            new DisjunctionRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.AreEqual(streamStartPosition, stream.Position);
        }
    }
}