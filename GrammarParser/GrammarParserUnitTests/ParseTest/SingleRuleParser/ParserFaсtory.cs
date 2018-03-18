using GrammarParser.Lexer.Parser.Classes.RuleParsers;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;

namespace GrammarParserUnitTests.ParseTest.SingleRuleParser {

    public interface IParserFaсtory {
        IParser GetNewParser();
    }

    public class ZeroOrManyParserFactory: IParserFaсtory {
        public IParser GetNewParser() => new ZeroOrManyParser();
    }

    public class OneOrManyParserFactory : IParserFaсtory {
        public IParser GetNewParser() => new OneOrManyParser();
    }

    public class OneOrZeroParserFactory : IParserFaсtory {
        public IParser GetNewParser() => new OneOrZeroParser();
    }
}
