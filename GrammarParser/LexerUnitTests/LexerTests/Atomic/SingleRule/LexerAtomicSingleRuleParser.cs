using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.Lexer;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Library.Extensions;
using GrammarParser.RuleLexer.Exceptions;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests.LexerTests.Atomic.SingleRule {

    public abstract class LexerAtomicSingleRuleParser {

        public abstract ILexer Lexer { get; }

        public abstract char Symbol { get; }

        public abstract Type CurrentRuleType { get; }

        [TestMethod]
        public void TestThatLexerSuccessParseOneSymbol() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'{this.Symbol}");
            var lexer = this.Lexer;

            // Act

            var parsedRules = lexer.Parse(stream).ParsedRules.ToList();
            var argument = (parsedRules.First() as ISingleArgumentRule)?.ArgumentRule;

            // Assert

            Assert.AreEqual(1, parsedRules.Count);
            Assert.IsInstanceOfType(parsedRules.First(), this.CurrentRuleType);

            Assert.IsInstanceOfType(argument, typeof(SymbolRule));
            Assert.AreEqual(symbol, (argument as SymbolRule).Symbol);
        }

        [TestMethod]
        public void TestThatLexerSuccessParseManyExistedSymbols()
        {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            var lexer = this.Lexer;

            // Act

            var parsedRules = lexer.Parse(stream).ParsedRules
                .AsEnumerable()
                .Reverse()
                .ToList();

            // Assert

            Assert.AreEqual(str.Count, parsedRules.Count);
            parsedRules.ForEach(x => Assert.IsInstanceOfType(x, this.CurrentRuleType));
            for (var i = 0; i < str.Count; i++)
            {
                var ruleSymbol = ((parsedRules[i] as ISingleArgumentRule)?.ArgumentRule as SymbolRule).Symbol;
                Assert.AreEqual(str[i], ruleSymbol);
            }
        }

        [TestMethod]
        public void TestThatLexerFailedParseWithOneExcessSymbol() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped + "a");
            var lexer = this.Lexer;

            // Act

            var parsedRules = new Action(() => lexer.Parse(stream));

            // Assert

            Assert.ThrowsException<ArgumentOutOfRangeException>(parsedRules);
        }

        [TestMethod]
        public void TestThatLexerSuccessParseGroupAsWraper() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString($"({mapped}){this.Symbol}");
            var lexer = this.Lexer;

            // Act

            var parseResult = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(1, parseResult.Count);
            Assert.IsInstanceOfType(parseResult[0], this.CurrentRuleType);
            Assert.IsInstanceOfType((parseResult[0] as ISingleArgumentRule).ArgumentRule, typeof(GroupRule));
        }

        [TestMethod]
        public void TestThatLexerSuccessParseGroupAsInternal() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString($"{mapped}({mapped}){this.Symbol}{mapped}");
            var lexer = this.Lexer;

            // Act

            var parseResult = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(str.Count * 2 + 1, parseResult.Count);
        }

        [TestMethod]
        public void TestThatLexerFailedParseGroupWithoutEnd()
        {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString($"{mapped}({mapped}{this.Symbol}{mapped}");
            var lexer = this.Lexer;

            // Act

            var action = new Action(() => lexer.Parse(stream));

            // Assert

            Assert.ThrowsException<LexerBadEndGroupDeclarationException>(action);
        }
    }
}