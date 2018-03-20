using System;

using GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public class RangeRuleParser : TwoArgumentRuleParser {

        public const string Symbol = "..";

        protected override string TerminateSymbol => Symbol;

        public override IRule Parse(IParserImmutableContext context) {
            var leftArgument = this.TryParse(context);

            IRule rightArgument;
            try {
                rightArgument = this.ParseRightArgument(context);
            }
            catch (ArgumentOutOfRangeException) {
                throw new CantParseRightArgumentException(this.TerminateSymbol, context);
            }


            if (leftArgument is SymbolRule left && rightArgument is SymbolRule right) {
                return new RangeRule(left, right);
            }

            throw new BadArgumentInRangeRuleException(context);
        }

    }

}