using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest.SingleRuleParser {

    [TestClass]
    public class OneOrZeroParserUnitTest: BaseSingleRuleParserUnitTest {
        public OneOrZeroParserUnitTest(): base(OneOrZeroParser.Symbol.ToString(), new OneOrZeroParserFactory()) { }
    }
}
