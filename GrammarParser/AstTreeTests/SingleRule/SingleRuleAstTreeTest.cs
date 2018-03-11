using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree;
using GrammarParser.Lexer;

using GrammarParserUnitTests.Utils;

using LexerUnitTests.LexerTests.Atomic.SingleRule;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AstTreeTests.SingleRule {

    public abstract class SingleRuleAstTreeTest {

        public abstract ILexer Lexer { get; }

        public abstract char Symbol { get; }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckSuccessedWithOneSymbol() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'{this.Symbol}");
            var testStream = new MemoryStream().FromString($"{symbol}");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsTrue(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckSuccessedWithWrongString() {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'{this.Symbol}");
            var testStream = new MemoryStream().FromString($"{symbol + 1}");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithOneSymbol() {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsTrue(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithManySymbol() {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str.Select( x=> $"{x}{x}{x}{x}{x}{x}{x}{x}{x}{x}")));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsTrue(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckFailedWithExcessedLastSymbol() {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            str.Add('z');
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str.Select(x => $"{x}{x}{x}{x}{x}{x}{x}{x}{x}{x}")));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

    }
}
