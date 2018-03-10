// GrammarParserUnitTests
// SymbolParserTest.cs
// Created 18.02.2018
// By Александр Кравченков

using System;
using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParserUnitTests.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest {

    [TestClass]
    public class SymbolParserUnitTest {

        #region Sucess parsing test

        [TestMethod]
        public void TestParseRuleSuccessWithOneSymbol() {

            // Arrange

            var stream = new MemoryStream().FromString("\'t\'");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var result = parser.Parse(context);

            // Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestParseRuleSuccessWithManySymbols() {

            // Arrange

            var stream = new MemoryStream().FromString("\'t\'sdjhfgjhsdgf");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var result = parser.Parse(context);

            // Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestParseRuleSuccessManySymbols() {

            // Arrange

            var stream = new MemoryStream().FromString("\'t\'\'t\'\'t\'\'t\'\'t\'");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act
            var result = new List<IRule> {
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context)
            };


            // Assert

            result.ForEach(Assert.IsNotNull);
        }

        #endregion

        #region Fail parsing test

        [TestMethod]
        public void TestParseRuleFailWithOneSymbol() {

            // Arrange

            var stream = new MemoryStream().FromString("a");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var result = parser.Parse(context);

            // Assert

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestParseRuleFailWithManySymbols() {

            // Arrange

            var stream = new MemoryStream().FromString("asdfsdf");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var result = parser.Parse(context);

            // Assert

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestParseRuleFailWithSymbolParseEmptySymbolException() {

            // Arrange

            var stream = new MemoryStream().FromString("\'\'werwer");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var action = new Action(() => { parser.Parse(context); });
        

            // Assert
            Assert.ThrowsException<SymbolParseEmptySymbolException>(action);
        }


        [TestMethod]
        public void TestParseRuleFailWithSymbolParserTooMuchSymbolsException() {

            // Arrange

            var stream = new MemoryStream().FromString("\'asfasdasdasd\'werwer");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var action = new Action(() => { parser.Parse(context); });


            // Assert
            Assert.ThrowsException<SymbolParserTooMuchSymbolsException>(action);
        }

        #endregion

        #region Position cheking tests

        [TestMethod]
        public void TestPositionAfterMultiParsing() {

            // Arrange

            var stream = new MemoryStream().FromString("\'t\'\'t\'\'t\'\'t\'\'t\'");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();

            // Act

            var startStreamPos = stream.Position;

            var result = new List<IRule> {
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context),
                parser.Parse(context)
            };

            var endStreamPosition = stream.Position;


            // Assert

            Assert.AreEqual(startStreamPos + (result.Count * 3), endStreamPosition);
        }

        [TestMethod]
        public void TestPositionAfterFailedParsing() {

            // Arrange

            var stream = new MemoryStream().FromString("a");
            var context = new DefaultParserContext(stream: stream);
            var parser = new SymbolParser();


            // Act

            var startPos = stream.Position;

            parser.Parse(context);
            var endPos = stream.Position;

            // Assert

            Assert.AreEqual(endPos, startPos);
        }

        #endregion
    }

}