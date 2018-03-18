using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree;
using GrammarParser.Lexer;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Library.Extensions;


using LexerUnitTests.LexerTests.Atomic.SingleRule;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AstTreeTests.SingleRule {

    [TestClass]
    public class OneOrManyRuleAstTreeTest : SingleRuleAstTreeTest {
        public override ILexer Lexer => new LexerAtomicOneOrManyParser().Lexer;
        public override char Symbol => new LexerAtomicOneOrManyParser().Symbol;

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromOneSymbolAndCheckSuccessedWithEmptyString()
        {

            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"\'{symbol}\'{this.Symbol}");
            var testStream = new MemoryStream().FromString($"");
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithOutFirstSymbol()
        {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            str.RemoveAt(0);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithOutSecondSymbol()
        {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            str.RemoveAt(1);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithOutLastSymbol()
        {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            str.RemoveAt(str.Count - 1);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

        [TestMethod]
        public void TestThatLexerGenerateRightTreeFromSeveralSymbolsAndCheckSuccessedWithoutFirstSymbol()
        {

            // Arrange

            var str = new List<char> { 'a', 'b', 'c', 'd' };
            var mapped = string.Join(string.Empty, str.Select(x => $"\'{x}\'{this.Symbol}"));
            var stream = new MemoryStream().FromString(mapped);
            str.RemoveAt(0);
            var testStream = new MemoryStream().FromString(string.Join(string.Empty, str.Select(x => $"{x}{x}{x}{x}{x}{x}{x}{x}{x}{x}")));
            var lexer = this.Lexer;

            // Act

            var tree = new AstTree(lexer.Parse(stream));

            // Assert

            Assert.IsFalse(tree.Check(testStream));
        }

    }
}
