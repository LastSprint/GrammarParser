using System;
using System.IO;

using GrammarParser.Lexer.Injections.Injectors;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Library.Extensions;
using GrammarParser.RuleLexer.Configurations;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest.TwoArgumentRuleParsers {

    [TestClass]
    public class RangeRuleParserUnitTest {

        #region Easy cases

        [TestMethod]
        public void TestThatSuccessCanParsing() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}\'{symbol2}\'");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);
            var leftArgument = new SymbolRule(symbol1);

            context.ParsedRules.Push(leftArgument);
            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var canParsed = parser.IsCurrentRule(context);

            // Assert

            Assert.IsTrue(canParsed);
        }

        [TestMethod]
        public void TestThatSuccessParsing() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}\'{symbol2}\'");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);
            var leftArgument = new SymbolRule(symbol1);

            context.ParsedRules.Push(leftArgument);
            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var parsed = parser.Parse(context);

            // Assert

            Assert.IsInstanceOfType(parsed, typeof(RangeRule));
            var converted = (RangeRule) parsed;
            Assert.IsNotNull(converted.LeftArgumentRule);
            Assert.IsNotNull(converted.RightArgumentRule);

            var leftConverted = (SymbolRule)converted.LeftArgumentRule;
            var rightConverted = (SymbolRule)converted.RightArgumentRule;

            Assert.AreSame(leftArgument, leftConverted);

            Assert.AreEqual(leftConverted.Symbol, symbol1);
            Assert.AreEqual(rightConverted.Symbol, symbol2);

            Assert.IsNotNull(converted.LeftArgumentRule);
        }

        [TestMethod]
        public void TestThatFailedChekingWithoutLastSymbol(){

            // Arrange

            var symbol1 = 'a';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);
            var leftArgument = new SymbolRule(symbol1);

            context.ParsedRules.Push(leftArgument);
            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var canParsed = parser.IsCurrentRule(context);

            // Assert

            Assert.IsFalse(canParsed);
        }

        [TestMethod]
        public void TestThatFailedParsingWithoutLastSymbol() {

            // Arrange

            var symbol1 = 'a';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);
            var leftArgument = new SymbolRule(symbol1);

            context.ParsedRules.Push(leftArgument);
            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var action = new Action(() => parser.Parse(context) );

            // Assert

            Assert.ThrowsException<CantParseRightArgumentException>(action);
        }

        [TestMethod]
        public void TestThatFailedChekingWithoutFirstSymbol() {

            // Arrange

            var symbol1 = 'a';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}\'{symbol1}\'");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);

            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var canParsed = parser.IsCurrentRule(context);

            // Assert

            Assert.IsFalse(canParsed);
        }

        [TestMethod]
        public void TestThatFailedParsingWithoutFirstSymbol() {

            // Arrange

            var symbol1 = 'a';

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}\'{symbol1}\'");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);

            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var action = new Action(() => parser.Parse(context));

            // Assert

            Assert.ThrowsException<RuleParserNotExistedLeftArgumentException>(action);
        }

        [TestMethod]
        public void TestThatFailedChekingWithoutArguments() {

            // Arrange

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);

            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var canParsed = parser.IsCurrentRule(context);

            // Assert

            Assert.IsFalse(canParsed);
        }

        [TestMethod]
        public void TestThatFailedParsingWithoutArguments() {

            // Arrange

            var stream = new MemoryStream().FromString($"{RangeRuleParser.Symbol}");
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);

            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            var action = new Action(() => parser.Parse(context));

            // Assert

            Assert.ThrowsException<RuleParserNotExistedLeftArgumentException>(action);
        }

        #endregion

        #region Test side-effects

        [TestMethod]
        public void TestThatAfterSuccessParsingStreamCursorHasRightPosition() {

            // Arrange

            var symbol1 = 'a';
            var symbol2 = 'c';

            var additionRule = $"\'{symbol2}\'";
            var str = $"{RangeRuleParser.Symbol}\'{symbol2}\'{additionRule}";

            var stream = new MemoryStream().FromString(str);
            var parser = new RangeRuleParser();

            var context = new DefaultParserContext(stream: stream);
            var leftArgument = new SymbolRule(symbol1);

            context.ParsedRules.Push(leftArgument);
            context.LexerBuilder = new LexerBuilder(new SimpleParserInjector());

            // Act

            parser.Parse(context);

            // Assert

            Assert.AreEqual(str.Length - additionRule.Length, context.CurrentStream.Position);
        }

        #endregion

    }
}
