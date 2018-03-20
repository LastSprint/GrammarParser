using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Library.Extensions;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.RuleTests.TwoArgumentRuleTests {

    [TestClass]
    public class RangeRuleTests {

        [TestMethod]
        public void TestRulePriority() {

            var rule = new RangeRule(null , null);

            Assert.AreEqual(rule.Priority, RulePriority.RuleRange);
        }

        [TestMethod]
        public void TestChekingSucessInNormalCase() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("b");

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestChekingSucessInLeftEdgeCase() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString(symbol1.ToString());

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestChekingSucessInRightEdgeCase() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString(symbol2.ToString());

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }



        [TestMethod]
        public void TestChekingSucessInCaseOfSameSymbols() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'a';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("a");

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsTrue(isCheked);
        }

        [TestMethod]
        public void TestChekingFailedInNormalCase() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'b';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("c");

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsFalse(isCheked);
        }

        [TestMethod]
        public void TestChekingFailedInBadRange() {

            // Arrange

            var symbol1 = 'c';
            var symbol2 = 'a';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("b");

            // Act

            var isCheked =
                new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.IsFalse(isCheked);
        }

        [TestMethod]
        public void TestThatStreamCursorHasRightPositionAfterSuccessParse() {
            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';
            var addition = 'e';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString($"b{addition}");

            // Act

            var streamStartPosition = stream.Position;

            new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.AreEqual(addition, stream.CurrentSymbol());
            Assert.AreEqual(streamStartPosition + 1, stream.Position);
        }

        [TestMethod]
        public void TestThatStreamCursorHasRightPositionAfterFalidParse() { 
            // Arrange

            var symbol1 = 'c';
            var symbol2 = 'a';

            var firstSymbolRule = new SymbolRule(symbol1);
            var secondSymbolRule = new SymbolRule(symbol2);

            var stream = new MemoryStream().FromString("b");

            // Act

            var streamStartPosition = stream.Position;

            new RangeRule(leftArgumentRule: firstSymbolRule, rightArgumentRule: secondSymbolRule).Check(stream);


            // Assert

            Assert.AreEqual(streamStartPosition, stream.Position);
        }

    }
}
