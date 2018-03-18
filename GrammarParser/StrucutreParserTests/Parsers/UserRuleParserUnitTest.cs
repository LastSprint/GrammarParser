using System;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.StructureLexer.Parsers;
using GrammarParser.Lexer.StructureLexer.Parsers.Exceptions;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrucutreParserTests.Parsers {

    [TestClass]
    public class UserRuleParserUnitTest {

        [TestMethod]
        public void TestSuccessChecking() {

            // Arrange

            var name = "Name";
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"Name\"{UserRuleParser.TokenExpressionDivider} first{UserRuleParser.TokenKeyValueDivider} 1{UserRuleParser.TokenExpressionDivider} second{UserRuleParser.TokenKeyValueDivider} 2";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var checkResult = parser.IsCurrentRule(context);


            // Assert

            Assert.IsTrue(checkResult);
        }

        [TestMethod]
        public void TestSuccessParsing() {

            // Arrange

            var name = "Name";
            var tokenName = "OLOLOLOL";
            var firstArg = "first";
            var secondArg = "second";
            var firstVal = 1;
            var secondVal = 2;
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"{tokenName}\"{UserRuleParser.TokenExpressionDivider} {firstArg}{UserRuleParser.TokenKeyValueDivider} {firstVal}{UserRuleParser.TokenExpressionDivider} {secondArg}{UserRuleParser.TokenKeyValueDivider} {secondVal}";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var result = parser.Parse(context) as UserRule;


            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
            Assert.AreEqual(pattern.Trim(), result.RulePattern);
            Assert.AreEqual(tokenName.Trim(), result.TokenConvertionPattern.Name);
            Assert.AreEqual(2, result.TokenConvertionPattern.Childs.Values.Count);
        }

        [TestMethod]
        public void TestFailedRuleParsing() {

            // Arrange

            var name = "Name";
            var tokenName = "OLOLOLOL";
            var firstArg = "first";
            var secondArg = "second";
            var firstVal = 1;
            var secondVal = 2;
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"{tokenName}\"{UserRuleParser.TokenExpressionDivider} {firstArg}{UserRuleParser.TokenKeyValueDivider} {firstVal}{UserRuleParser.TokenExpressionDivider} {secondArg}{UserRuleParser.TokenKeyValueDivider} {secondVal}";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var action = new Action(() => parser.Parse(context));


            // Assert

            Assert.ThrowsException<UserRuleParserBadRulePatternException>(action);
        }

        [TestMethod]
        public void TestFailedTokenExpressionWithoutNameParsing() {

            // Arrange

            var name = "Name";
            var firstArg = "first";
            var secondArg = "second";
            var firstVal = 1;
            var secondVal = 2;
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{firstArg}{UserRuleParser.TokenKeyValueDivider} {firstVal}{UserRuleParser.TokenExpressionDivider} {secondArg}{UserRuleParser.TokenKeyValueDivider} {secondVal}";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var action = new Action(() => parser.Parse(context));


            // Assert

            Assert.ThrowsException<UserRuleParserBadTokenExpressionException>(action);
        }

        [TestMethod]
        public void TestFailedTokenExpressionWithoutDevidereParsing() {

            // Arrange

            var name = "Name";
            var firstArg = "first";
            var secondArg = "second";
            var tokenName = "OLOLOLOL";
            var firstVal = 1;
            var secondVal = 2;
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"{tokenName}\" {firstArg}{UserRuleParser.TokenKeyValueDivider} {firstVal}{UserRuleParser.TokenExpressionDivider} {secondArg}{UserRuleParser.TokenKeyValueDivider} {secondVal}";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var action = new Action(() => parser.Parse(context));


            // Assert

            Assert.ThrowsException<UserRuleParserBadTokenExpressionException>(action);
        }

        [TestMethod]
        public void TestFailedTokenExpressionWithBadNumbereParsing(){

            // Arrange

            var name = "Name";
            var firstArg = "first";
            var secondArg = "second";
            var tokenName = "OLOLOLOL";
            var firstVal = "a";
            var secondVal = 2;
            var pattern =
                "((\'a\'..\'z\')|(\'A\'..\'Z\')|(\'0\'..\'9\'+))+\' \'*\':\'\' \'*(\'!\'..\':\'+)|(\'<\'..\'~\')\';\' ";
            var token = $"{UserRuleParser.NameTokenString}{UserRuleParser.TokenKeyValueDivider} \"{tokenName}\"{UserRuleParser.TokenExpressionDivider} {firstArg}{UserRuleParser.TokenKeyValueDivider} {firstVal}{UserRuleParser.TokenExpressionDivider} {secondArg}{UserRuleParser.TokenKeyValueDivider} {secondVal}";

            var str =
                $"{name}{UserRuleParser.RuleNameEndTerminator}{pattern}{UserRuleParser.ConvertionOperator}{token}{UserRuleParser.RuleEndTerminator}";

            var stream = new MemoryStream().FromString(str);
            var context = new DefaultParserContext(stream: stream);
            var parser = new UserRuleParser();

            // Act

            var action = new Action(() => parser.Parse(context));


            // Assert

            Assert.ThrowsException<UserRuleParserBadTokenExpressionException>(action);
        }
    }
}
