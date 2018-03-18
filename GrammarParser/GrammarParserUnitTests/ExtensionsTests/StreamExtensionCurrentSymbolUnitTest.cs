using System.IO;

using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ExtensionsTests {

    [TestClass]
    public class StreamExtensionCurrentSymbolUnitTest {

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

    }
}
