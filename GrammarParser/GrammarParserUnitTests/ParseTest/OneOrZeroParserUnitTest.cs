using System;
using System.IO;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Types.Classes;

using GrammarParserUnitTests.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest {

    [TestClass]
    public class OneOrZeroParserUnitTest {

    #region Success cases

        [TestMethod]
        public void TestParseRuleSuccessWithOneSymbol() {

            // Arrange

            var stream = new MemoryStream().FromString(OneOrZeroParser.Symbol.ToString());
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();
            var leftArgument = new SymbolRule('b');
            context.ParsedRules.Push(leftArgument);

            // Act

            var checkResult = parser.IsCurrentRule(context);
            var result = parser.Parse(context);

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(checkResult);
        }

        [TestMethod]
        public void TestParseRuleSuccessWithFilledStack() {

            // Arrange

            var stream = new MemoryStream().FromString($"{OneOrZeroParser.Symbol}fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            var leftArgument1 = new SymbolRule('a');
            var leftArgument2 = new SymbolRule('b');
            var leftArgument3 = new SymbolRule('c');

            context.ParsedRules.Push(leftArgument1);
            context.ParsedRules.Push(leftArgument2);
            context.ParsedRules.Push(leftArgument3);

            // Act

            var streamStart = stream.Position;
            var checkResult = parser.IsCurrentRule(context);
            var result = parser.Parse(context) as OneOrZeroRule;

            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(checkResult);
            Assert.AreSame(result.ArgumentRule, leftArgument3);
            Assert.AreEqual(stream.Position, streamStart + 1);
        }

        [TestMethod]
        public void TestThatRuleArgumentIsCorrect() {

            // Arrange

            var stream = new MemoryStream().FromString($"{OneOrZeroParser.Symbol}fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            var leftArgument1 = new SymbolRule('a');
            var leftArgument2 = new SymbolRule('b');
            var leftArgument3 = new SymbolRule('c');

            context.ParsedRules.Push(leftArgument1);
            context.ParsedRules.Push(leftArgument2);
            context.ParsedRules.Push(leftArgument3);

            // Act

            
            var result = parser.Parse(context) as OneOrZeroRule;

            // Assert

            Assert.AreSame(result.ArgumentRule, leftArgument3);
        }

        [TestMethod]
        public void TestThatStreamPositionAfterSuccessedParsingIsCorrect() {

            // Arrange

            var stream = new MemoryStream().FromString($"{OneOrZeroParser.Symbol}fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            var leftArgument = new SymbolRule('a');

            context.ParsedRules.Push(leftArgument);

            // Act

            var streamStart = stream.Position;
            parser.Parse(context);

            // Assert

            Assert.AreEqual(stream.Position, streamStart + 1);
        }

        [TestMethod]
        public void TestThatCheckingtDoesNotAffectOnStreamPositionInSuccessCase() {

            // Arrange

            var stream = new MemoryStream().FromString($"{OneOrZeroParser.Symbol}fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            var leftArgument1 = new SymbolRule('a');

            context.ParsedRules.Push(leftArgument1);

            // Act

            var streamStart = stream.Position;
            parser.IsCurrentRule(context);

            // Assert

            Assert.AreEqual(stream.Position, streamStart);
        }

        #endregion

        #region Failed cases

        [TestMethod]
        public void TestThatCheckingtDoesNotAffectOnStreamPositionInFailedCaseWithoutSymbol() {

            // Arrange

            var stream = new MemoryStream().FromString($"fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            var leftArgument1 = new SymbolRule('a');

            context.ParsedRules.Push(leftArgument1);

            // Act

            var streamStart = stream.Position;
            parser.IsCurrentRule(context);

            // Assert

            Assert.AreEqual(stream.Position, streamStart);
        }

        [TestMethod]
        public void TestThatCheckingtDoesNotAffectOnStreamPositionInFailedCaseWithoutSymbolAndArgument() {

            // Arrange

            var stream = new MemoryStream().FromString($"fgadcbv");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            context.ParsedRules.Push(null);

            // Act

            var streamStart = stream.Position;
            parser.IsCurrentRule(context);

            // Assert

            Assert.AreEqual(stream.Position, streamStart);
        }

        [TestMethod]
        public void TestParseRuleFailedWithoutSymbol() {

            // Arrange

            var stream = new MemoryStream().FromString("hgasfdhgasdfghagshd");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();
            var leftArgument = new SymbolRule('b');
            context.ParsedRules.Push(leftArgument);

            // Act

            var checkResult = parser.IsCurrentRule(context);
            var result = parser.Parse(context);

            // Assert

            Assert.IsNull(result);
            Assert.IsFalse(checkResult);
        }

        [TestMethod]
        public void TestThrowsNullLeftArgumentException()
        {

            // Arrange

            var stream = new MemoryStream().FromString("hgasfdhgasdfghagshd");
            var context = new DefaultParserContext(stream: stream);
            var parser = new OneOrZeroParser();

            // Act

            var action = new Action(() => { parser.Parse(context); });

            // Assert

            Assert.ThrowsException<RuleParserNotExistedLeftArgumentException>(action);
        }

        #endregion
    }
}
