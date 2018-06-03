using System.IO;
using System.Linq;

using GrammarParser.Lexer.StructureLexer.Parsers;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrucutreParserTests.Groups {

    [TestClass]
    public class GroupParserTests {

        [TestMethod]
        public void TestThatParseSuccess() {
            // Arrange

            var text = "block Rule {\r\n    space: \'\r\'|\'\n\'|\'\t\'|\' \' => Name: \"space\", first:1;\r\n}";
            var stream = new MemoryStream().FromString(text);

            // Act

            var parser = new GroupParser();
            var result = parser.Parse(stream);

            // Arrange

            Assert.AreEqual(1, result.ParsedRules.Count);
        }

        [TestMethod]
        public void TestThatParseManyRulesSuccess() {
            // Arrange

            var name1 = "space";
            var name2 = "test";
            var text = "block Rule {\r\n    space: \'\r\'|\'\n\'|\'\t\'|\' \' => Name: \"space\", first:1;\r\n" +
                       "test: \'a\'|\'b\' => Name: \"test\", furst:1;\r\n" +
                       "}";
            var stream = new MemoryStream().FromString(text);

            // Act

            var parser = new GroupParser();
            var result = parser.Parse(stream);

            // Arrange

            Assert.AreEqual(2, result.ParsedRules.Count);
            Assert.AreEqual(name2, (result.ParsedRules.First() as UserRule)?.Name);
            Assert.AreEqual(name1, (result.ParsedRules.Last() as UserRule)?.Name);
        }

        [TestMethod]
        public void TestThatParseManyRulesWhereOneUseAnotherSuccess()
        {
            // Arrange

            var name1 = "space";
            var name2 = "test";
            var text = "block Rule {\r\n    space: \'\r\'|\'\n\'|\'\t\'|\' \' => Name: \"space\", first:1;\r\n" +
                       "test: \'a\'|\'a\' => Name: \"test\", furst:1;\r\n" +
                       "}";
            var stream = new MemoryStream().FromString(text);

            // Act

            var parser = new GroupParser();
            var result = parser.Parse(stream);

            // Arrange

            Assert.AreEqual(2, result.ParsedRules.Count);
            Assert.AreEqual(name2, (result.ParsedRules.First() as UserRule)?.Name);
            Assert.AreEqual(name1, (result.ParsedRules.Last() as UserRule)?.Name);
        }

    }
}
