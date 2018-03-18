using System;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public class RangeRuleParser: TwoArgumentRuleParser {

        public const string Symbol = "..";

        protected override string TerminateSymbol => RangeRuleParser.Symbol;

        public override IRule Parse(IParserImmutableContext context) {
            var leftArgument = base.TryParse(context);

            IRule rightArgument;
            try {
                 rightArgument = this.ParseRightArgument(context);
            } catch (ArgumentOutOfRangeException) {
                throw new CantParseRightArgumentException(ruleSymbol: this.TerminateSymbol, context: context);
            }
            

            if (leftArgument is SymbolRule left && rightArgument is SymbolRule right) {
                return new RangeRule(left, right); 
            }

            throw new BadArgumentInRangeRuleException(context: context);
        }
    }
}
