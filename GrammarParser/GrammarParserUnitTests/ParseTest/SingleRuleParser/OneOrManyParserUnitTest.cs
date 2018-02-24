using GrammarParser.Lexer.Parser.Classes.RuleParsers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrammarParserUnitTests.ParseTest.SingleRuleParser {

    [TestClass]
    public class OneOrManyParserUnitTest: BaseSingleRuleParserUnitTest {
        public OneOrManyParserUnitTest(): base(OneOrManyParser.Symbol.ToString(), new OneOrManyParserFactory()) { }
    }
}