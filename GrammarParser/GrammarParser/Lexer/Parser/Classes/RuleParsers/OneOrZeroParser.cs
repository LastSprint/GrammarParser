﻿using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers {

    public class OneOrZeroParser: SingleRuleParser {

        public const char Symbol = '?';

        protected override string TerminateSymbol => Symbol.ToString();

        public override IRule Parse(IParserImmutableContext context) {
            var result = base.TryParse(context);

            return result == null ? null : new OneOrZeroRule(result);
        }

    }
}
