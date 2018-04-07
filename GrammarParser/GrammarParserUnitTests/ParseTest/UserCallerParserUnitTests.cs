using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.RuleLexer.Parser.Classes;
using GrammarParser.Lexer.RuleLexer.Parser.Exceptions;
using GrammarParser.Lexer.StructureLexer.Models;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest {

    [TestClass]
    public class UserCallerParserUnitTests {

        [TestMethod]
        public void TestThatUserRuleParsingSucces() {
            
            // Arrange

            var ruleName = "example0Rul1";
            var stream = new MemoryStream().FromString(ruleName);


            var rule = new UserRule(ruleName, "", new TokenExpression("", new Dictionary<string, int>()));
            var context = new DefaultParserContext(stream);
            context.ParsedRules.Push(rule);
            var ruleParser = new RuleCallParser();

            // Act

            var parsed = ruleParser.Parse(context);

            // Assert

            Assert.IsNotNull(parsed);
            Assert.AreSame(rule, parsed);
        }

        [TestMethod]
        public void TestThatUserRuleParsingFailedBecaouseOfStackIsEmpty() {

            // Arrange

            var ruleName = "example0Rul1";
            var stream = new MemoryStream().FromString(ruleName);


            var context = new DefaultParserContext(stream);
            var ruleParser = new RuleCallParser();

            // Act

            var action = new Action(() => ruleParser.Parse(context));

            // Assert

            Assert.ThrowsException<BadRuleNameException>(action);
        }

        [TestMethod]
        public void TestThatUserRuleParsingFailedBecaouseOfUbdefindName() {

            // Arrange

            var ruleName = "example0Rul1";
            var stream = new MemoryStream().FromString(ruleName);


            var context = new DefaultParserContext(stream);
            var ruleParser = new RuleCallParser();

            // Act

            var action = new Action(() => ruleParser.Parse(context));

            // Assert

            Assert.ThrowsException<BadRuleNameException>(action);
        }

        [TestMethod]
        public void TestThatUserRuleParsingSuccessChoose() {

            // Arrange

            var ruleName = "example0Rul1";
            var stream = new MemoryStream().FromString(ruleName);


            var rule1 = new UserRule(ruleName, "", new TokenExpression("", new Dictionary<string, int>()));
            var rule2 = new UserRule(new string(ruleName.ToCharArray().Reverse().ToArray()), "", new TokenExpression("", new Dictionary<string, int>()));
            var context = new DefaultParserContext(stream);
            context.ParsedRules.Push(rule1);
            context.ParsedRules.Push(rule2);
            var ruleParser = new RuleCallParser();

            // Act

            var parsed = ruleParser.Parse(context);

            // Assert

            Assert.IsNotNull(parsed);
            Assert.AreSame(parsed, rule1);
        }

        [TestMethod]
        public void TestThatUserRuleParsingSuccesAndStreamPositionSuccess() {

            // Arrange

            var ruleName = "example0Rul1";
            var stream = new MemoryStream().FromString(ruleName + "\'");


            var rule = new UserRule(ruleName, "", new TokenExpression("", new Dictionary<string, int>()));
            var context = new DefaultParserContext(stream);
            context.ParsedRules.Push(rule);
            var ruleParser = new RuleCallParser();

            // Act

            var startPos = stream.Position;

            var parsed = ruleParser.Parse(context);

            var endPos = stream.Position;

            // Assert

            Assert.AreEqual(endPos, ruleName.Length + startPos);
        }
    }
}
