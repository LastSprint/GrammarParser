using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.Lexer;
using GrammarParser.Lexer.Configurations;
using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Injections.Injectors.Atomic;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Rules.Classes;

using GrammarParserUnitTests.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests {
    [TestClass]
    public class LexerOnlyWithSymbolParser {

        private ILexer _lexer => new Lexer(new SymbolParserInjector().Injection(),
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
            parsers?.Parsers.ToList().ForEach(x => Assert.IsInstanceOfType(x, typeof(SymbolParser)));
        }

        [TestMethod]
        public void TestThatLexerSuccessParseOneSymbol() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'");
            var lexer = this._lexer;

            // Act

            var parsedRules = lexer.Parse(stream).ParsedRules.ToList();

            // Assert

            Assert.AreEqual(1, parsedRules.Count);
            Assert.IsInstanceOfType(parsedRules.First(), typeof(SymbolRule));
            Assert.AreEqual(symbol, (parsedRules.First() as SymbolRule).Symbol);
        }

        [TestMethod]
        public void TestThatLexerSuccessParseMany(){

            // Arrange
            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString(mapped);
            var lexer = this._lexer;

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
            var lexer = this._lexer;

            // Act

            var parsedRules = new Action(() => lexer.Parse(stream));

            // Assert

            Assert.ThrowsException<ArgumentOutOfRangeException>(parsedRules);
        }
    }
}
