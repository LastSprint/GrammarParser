using GrammarParser.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.LibraryTests {

    [TestClass]
    public class CharRangeTest {

        [TestMethod]
        public void TestThatSimpleRangeWorkSuccess() {
            
            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol);

            // Act

            var isContained = range.Contains('b');

            // Assert

            Assert.IsTrue(isContained);
        }

        [TestMethod]
        public void TestThatSimpleRangeFailed() {

            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol);

            // Act

            var isContained = range.Contains('q');

            // Assert

            Assert.IsFalse(isContained);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithoutEdgeSymbols() {

            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol);

            // Act

            var isContained1 = range.Contains(startSymbol);
            var isContained2 = range.Contains(endSymbol);

            // Assert

            Assert.IsFalse(isContained1);
            Assert.IsFalse(isContained2);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithLeftEdgeSymbols() {

            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol, isStartEqual: true, isEndEqual: false);

            // Act

            var isContained1 = range.Contains(startSymbol);
            var isContained2 = range.Contains(endSymbol);

            // Assert

            Assert.IsTrue(isContained1);
            Assert.IsFalse(isContained2);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithEndEdgeSymbols() {

            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol, isStartEqual: false, isEndEqual: true);

            // Act

            var isContained1 = range.Contains(startSymbol);
            var isContained2 = range.Contains(endSymbol);

            // Assert

            Assert.IsFalse(isContained1);
            Assert.IsTrue(isContained2);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithAllEdgeSymbols() {

            // Arrange

            var startSymbol = 'a';
            var endSymbol = 'c';
            var range = new CharRange(startSymbol, endSymbol, isStartEqual: true, isEndEqual: true);

            // Act

            var isContained1 = range.Contains(startSymbol);
            var isContained2 = range.Contains(endSymbol);

            // Assert

            Assert.IsTrue(isContained1);
            Assert.IsTrue(isContained2);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithutLeftLimitSuccess() {

            // Arrange
            var endSymbol = 'c';
            var range = new CharRange(null, endSymbol);

            // Act

            var isContained = range.Contains((char)(endSymbol - 1));

            // Assert
            
            Assert.IsTrue(isContained);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithutLeftLimitFailed() {

            // Arrange
            var endSymbol = 'c';
            var range = new CharRange(null, endSymbol);

            // Act

            var isContained = range.Contains((char)(endSymbol + 1));

            // Assert

            Assert.IsFalse(isContained);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithoutRightLimitSuccess() {

            // Arrange
            var startSymbol = 'c';
            var range = new CharRange(startSymbol, null);

            // Act

            var isContained = range.Contains((char)(startSymbol + 1));

            // Assert

            Assert.IsTrue(isContained);
        }

        [TestMethod]
        public void TestThatSimpleRangeWithoutRightLimitFailed() {

            // Arrange
            var startSymbol = 'c';
            var range = new CharRange(startSymbol, null);

            // Act

            var isContained = range.Contains((char)(startSymbol - 1));

            // Assert

            Assert.IsFalse(isContained);
        }

    }
}