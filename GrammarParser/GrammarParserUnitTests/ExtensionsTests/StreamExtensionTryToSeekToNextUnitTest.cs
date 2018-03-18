using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GrammarParser.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ExtensionsTests {

    [TestClass]
    public class StreamExtensionTryToSeekToNextUnitTest {

        [TestMethod]
        public void TestThatNonEmpyStreamSeekedCorrectly() {

            // Arrange

            var char1 = '1';
            var char2 = '2';

            Stream stream = new MemoryStream().FromString($"{char1}{char2}");

            // Act

            var fisrtRead = stream.CurrentSymbol();
            stream.TryToSeekToNext();
            var seconRead = stream.CurrentSymbol();

            // Assert

            Assert.AreEqual(char1, fisrtRead);
            Assert.AreEqual(char2, seconRead);

        }

        [TestMethod]
        public void TestThatNonEmpyStreamSeekedInCorrectly() {

            // Arrange

            var char1 = '1';
            var char2 = '2';

            Stream stream = new MemoryStream().FromString($"{char1}a{char2}");

            // Act

            var fisrtRead = stream.CurrentSymbol();
            stream.TryToSeekToNext();
            var seconRead = stream.CurrentSymbol();

            // Assert

            Assert.AreEqual(char1, fisrtRead);
            Assert.AreNotEqual(char2, seconRead);
        }

        [TestMethod]
        public void TestThatEmptySreamSeekedWithoutException() {

            // Arrange

            var char1 = '1';
            var char2 = '2';

            Stream stream = new MemoryStream().FromString($"{char1}a{char2}");

            // Act

            stream.TryToSeekToNext();

            // Assert
            
        }

        [TestMethod]
        public void TestThatNoEmptySreamSeekedWithoutException() {

            // Arrange

            var char1 = '1';

            Stream stream = new MemoryStream().FromString($"{char1}");

            // Act

            stream.TryToSeekToNext();

            // Assert

        }
    }
}
