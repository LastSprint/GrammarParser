using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest.SingleRuleParser {

    [TestClass]
    public class ZeroOrManyParserUnitTest: BaseSingleRuleParserUnitTest {
        public ZeroOrManyParserUnitTest() : base(ZeroOrManyParser.Symbol.ToString(), new ZeroOrManyParserFactory()) { }
    }
}