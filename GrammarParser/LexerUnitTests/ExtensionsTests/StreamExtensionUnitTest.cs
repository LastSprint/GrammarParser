using System;
using System.IO;

using GrammarParser.Library.Extensions;

using GrammarParserUnitTests.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LexerUnitTests.ExtensionsTests {

    [TestClass]
    public class StreamExtensionUnitTest {

        [TestMethod]
        public void TestThatCharacterIsCorrectInStart() {
            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"{symbol}shgdfjsgdjhfgsybdfsndxfjhsgd");

            // Act

            var current = stream.CurrentSymbol();

            // Assert

            Assert.AreEqual(current, symbol);
        }

        [TestMethod]
        public void TestThatCharacterIsFaliedInStart() {
            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"v{symbol}shgdfjsgdjhfgsybdfsndxfjhsgd");

            // Act

            var current = stream.CurrentSymbol();

            // Assert

            Assert.AreNotEqual(current, symbol);
        }

        [TestMethod]
        public void TestThatStreamPositionNotChangeAfterSuccessReading() {
            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"${symbol}shgdfjsgdjhfgsybdfsndxfjhsgd");

            // Act
            var startPos = stream.Position;
            stream.CurrentSymbol();
            var endPos = stream.Position;

            // Assert

            Assert.AreEqual(startPos, endPos);
        }

        [TestMethod]
        public void TestThatStreamPositionNotChangeAfterFailedsReading() {
            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"a{symbol}shgdfjsgdjhfgsybdfsndxfjhsgd");

            // Act
            var startPos = stream.Position;
            stream.CurrentSymbol();
            var endPos = stream.Position;

            // Assert

            Assert.AreEqual(startPos, endPos);
        }

        [TestMethod]
        public void TestThatIfNonEmptyStreamIsEndThenMethodReturnNull() {
            // Arrange

            var symbol = 'a';
            var stream = new MemoryStream().FromString($"a{symbol}shgdfjsgdjhfgsybdfsndxfjhsgd");

            // Act
            stream.Position = stream.Length;
            var result = stream.CurrentSymbol();

            // Assert

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestThatIfEmptyStreamIsEndThenMethodReturnNull() {
            // Arrange

            var stream = new MemoryStream().FromString(value: string.Empty);

            // Act
            var result = stream.CurrentSymbol();

            // Assert

            Assert.IsNull(result);
        }

    }
}
