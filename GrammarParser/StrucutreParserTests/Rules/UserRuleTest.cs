 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
 using GrammarParser.Lexer.StructureLexer.Models;
 using GrammarParser.Lexer.StructureLexer.Rules;
 using GrammarParser.Library.Extensions;
 using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrucutreParserTests.Rules {

    [TestClass]
    public class UserRuleTest {
        private string rawRule = "'a'..'z'+' '+'a'..'z'+' '*';'";

        [TestMethod]
        public void TestThatSimpleRuleWorkCorrect() {

            // Arrange

            var text = "int a;";
            var name = "name";
            var stream = new MemoryStream().FromString(text);

            var tokenPatter = new TokenExpression(name, new Dictionary<string, int> {
                {"type", 0},
                {"name", 2}
            });

            var rule = new UserRule(name: name, rulePattern: this.rawRule, tokenConvertionPattern: tokenPatter);

            // Act

            rule.Check(stream);
            var result = rule.Convert();

            // Assert

            Assert.AreEqual(result.Name, name);
            Assert.IsNull(result.Value);
            Assert.AreEqual(2, result.Childs.Count);
            var type = result.Childs.FirstOrDefault(x => x.Name == "type");

            Assert.IsNotNull(type);
            Assert.AreEqual(0, type.Childs.Count);
            Assert.AreEqual("int", type.Value);
            var vrName = result.Childs.FirstOrDefault(x => x.Name == "name");

            Assert.IsNotNull(vrName);
            Assert.AreEqual(0, vrName.Childs.Count);
            Assert.AreEqual("a", vrName.Value);
        }
    }
}
