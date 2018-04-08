using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using GrammarParser.Lexer.StructureLexer.Parsers;
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

    }
}
