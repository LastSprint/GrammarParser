using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.StructureLexer.Parsers;
using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrucutreParserTests.Groups {

    [TestClass]
    public class RuleStructureTests {

        [TestMethod]
        public void TestThatSuccessParsingOneRule() {
            // Arrange

            var name = "Name";
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"Name\"{UserRuleParser.TokenExpressionDivider} first{UserRuleParser.TokenKeyValueDivider} 1{UserRuleParser.TokenExpressionDivider} second{UserRuleParser.TokenKeyValueDivider} 2";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}"+"}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleStructureParser(new UserRuleParser());
            // Act

            var checkResult = parser.Parse(context);


            // Assert

            Assert.AreEqual(checkResult.Count, 1);
            Assert.AreEqual(checkResult.First().Name, name);
        }

        [TestMethod]
        public void TestThatSuccessParsingManyRule()
        {
            // Arrange

            var name = "Name";
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";

            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"Name\"{UserRuleParser.TokenExpressionDivider} first{UserRuleParser.TokenKeyValueDivider} 1{UserRuleParser.TokenExpressionDivider} second{UserRuleParser.TokenKeyValueDivider} 2";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";
            str += str;
            str += str;
            str += "}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleStructureParser(new UserRuleParser());
            // Act

            var checkResult = parser.Parse(context);


            // Assert

            Assert.AreEqual(checkResult.Count, 4);
            Assert.AreEqual(checkResult.First().Name, name);
        }


    }
}
