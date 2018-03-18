using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.Lexer;
using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Library.Extensions;
using GrammarParser.RuleLexer;
using GrammarParser.RuleLexer.Configurations;
using GrammarParser.RuleLexer.Exceptions;
using GrammarParser.RuleLexer.Injections.Injectors.Atomic;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests.LexerTests.Atomic {
    [TestClass]
    public class LexerAtomicWithSymbolParser {

        public ILexer Lexer => new Lexer(new SymbolParserInjector().Injection(),
            new LexerBuilder(new SymbolParserInjector()));

        [TestMethod]
        public void TestThatInjectorConfiguredSuccessful() {

            // Arrange

            var injector = new SymbolParserInjector();

            // Act

            var parsers = injector.Injection() as ParserAgregator;

            // Assert

            Assert.IsNotNull(parsers);
            Assert.AreEqual(parsers.Parsers.Count, 1);
            parsers.Parsers.ToList().ForEach(x => Assert.IsInstanceOfType(x, typeof(SymbolParser)));
        }

        [TestMethod]
        public void TestThatLexerSuccessParseOneSymbol() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'");
            var lexer = this.Lexer;

            // Act

            var parsedRules = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(1, parsedRules.Count);
            Assert.IsInstanceOfType(parsedRules.First(), typeof(SymbolRule));
            Assert.AreEqual(symbol, ((SymbolRule) parsedRules.First()).Symbol);
        }

        [TestMethod]
        public void TestThatLexerSuccessParseMany(){

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString(mapped);
            var lexer = this.Lexer;

            // Act

            var parsedRules = lexer.Parse(stream).ParsedRules
                .AsEnumerable()
                .Reverse()
                .ToList();

            // Assert

            Assert.AreEqual(str.Count, parsedRules.Count);
            parsedRules.ForEach(x=> Assert.IsInstanceOfType(x, typeof(SymbolRule)));
            for (var i = 0; i < str.Count; i++) {
                Assert.AreEqual(str[i], (parsedRules[i] as SymbolRule)?.Symbol);
            }
        }

        [TestMethod]
        public void TestThatLexerFailedParseWithOneExcessSymbol() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
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
            var stream = new MemoryStream().FromString($"({mapped})");
            var lexer = this.Lexer;

            // Act

            var parseResult = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(1, parseResult.Count);
            Assert.IsInstanceOfType(parseResult[0], typeof(GroupRule));
            Assert.AreEqual(str.Count, (parseResult[0] as GroupRule).NestedRules.Count);
            foreach (var nestedRule in (parseResult[0] as GroupRule).NestedRules) {
                Assert.IsInstanceOfType(nestedRule, typeof(SymbolRule));
            }
        }

        [TestMethod]
        public void TestThatLexerSuccessParseGroupAsInternal() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString($"{mapped}({mapped}){mapped}");
            var lexer = this.Lexer;

            // Act

            var parseResult = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(str.Count * 2 + 1, parseResult.Count);
        }

        [TestMethod]
        public void TestThatLexerFailedParseGroupWithoutEnd() {

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString($"{mapped}({mapped}{mapped}");
            var lexer = this.Lexer;

            // Act

            var action = new Action(() => lexer.Parse(stream));

            // Assert

            Assert.ThrowsException<LexerBadEndGroupDeclarationException>(action);
        }
    }
}
