using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree;
using GrammarParser.Lexer;
using GrammarParser.Lexer.Configurations;
using GrammarParser.Lexer.Injections.Injectors.Atomic;
using GrammarParser.Lexer.Rules.Classes;

using GrammarParserUnitTests.Utils;

using LexerUnitTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AstTreeTests {

    [TestClass]
    public class SymbolAstStreeTests {

        public ILexer Lexer => new LexerOnlyWithSymbolParser().Lexer;

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckSuccessed() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'");
            var testStream = new MemoryStream().FromString($"{symbol}");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsTrue(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckFiledBecauseOfWrongSymbol() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'");
            var testStream = new MemoryStream().FromString($"{symbol + 1}");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckFiledBecauseOfManySymbols() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'");
            var testStream = new MemoryStream().FromString($"{symbol}{symbol}");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateSuccessTreeForManyRulesAndSchekeSuccessed() {

            // Arrange
            var str = new List<char> { 's', 'a', 's', 'h', 'e', 'c', 'h', 'k', 'a' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString(mapped);
            var testStream = new MemoryStream().FromString("sashechka");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsTrue(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateSuccessTreeForManyRulesAndSchekeFailed() {

            // Arrange
            var str = new List<char> { 's', 'a', 's', 'h', 'e', 'c', 'h', 'k', 'a' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'"));
            var stream = new MemoryStream().FromString(mapped);
            var testStream = new MemoryStream().FromString("sshechka");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }
    }
}
