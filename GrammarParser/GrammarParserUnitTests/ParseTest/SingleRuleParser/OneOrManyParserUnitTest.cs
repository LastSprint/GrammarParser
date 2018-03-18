using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.RuleLexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest.SingleRuleParser {

    [TestClass]
    public class OneOrManyParserUnitTest: BaseSingleRuleParserUnitTest {
        public OneOrManyParserUnitTest(): base(OneOrManyParser.Symbol.ToString(), new OneOrManyParserFactory()) { }
    }
}