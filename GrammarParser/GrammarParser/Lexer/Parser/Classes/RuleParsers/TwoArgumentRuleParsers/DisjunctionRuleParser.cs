using System;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public class DisjunctionRuleParser : TwoArgumentRuleParser {

        public const string Symbol = "|";

        protected override string TerminateSymbol => DisjunctionRuleParser.Symbol;

        public override IRule Parse(IParserImmutableContext context) {
            var leftArgument = base.TryParse(context);

            IRule rightArgument;
            try {
                rightArgument = this.ParseRightArgument(context);
            }
            catch (ArgumentOutOfRangeException) {
                throw new CantParseRightArgumentException(ruleSymbol: this.TerminateSymbol, context: context);
            }

            if (leftArgument != null && rightArgument != null) {
                return new DisjunctionRule(leftArgument, rightArgument);
            }

            return null;
        }

    }

}
