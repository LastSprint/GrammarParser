using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ExtensionsTests {

    [TestClass]
    public class StreamExtensionNextWithSkipedEmpty {

        [TestMethod]
        public void TextThatMEthodWorksSuccess() {
            // Arrange

            var symbol = 'v';
            var text = $"                               \r\n\t\r\n\r\r\r\r\n\r                           \r {symbol}";

            // Act

            var stream = new MemoryStream().FromString(text);

            var result = stream.NextWithSkipedEmpty();

            // Assert

            Assert.AreEqual(symbol, result);
        }

        [TestMethod]
        public void TextThatMethodFail(){
            // Arrange

            var text = $"                               \r\n\t\r\n\r\r\r\r\n\r                           \r ";

            // Act

            var stream = new MemoryStream().FromString(text);

            var result = stream.NextWithSkipedEmpty();

            // Assert

            Assert.IsNull(result);
        }
    }
}
