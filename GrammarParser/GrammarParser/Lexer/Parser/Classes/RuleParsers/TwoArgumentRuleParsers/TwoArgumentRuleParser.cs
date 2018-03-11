using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public abstract class TwoArgumentRuleParser: SingleRuleParser {

        public override bool IsCurrentRule(IParserImmutableContext context) {

            // TODO: Write logick after implement parsing

            if (!base.IsCurrentRule(context)) {
                return false;
            }

            return false;
        }

        public abstract override IRule Parse(IParserImmutableContext conext);

    }
}
  