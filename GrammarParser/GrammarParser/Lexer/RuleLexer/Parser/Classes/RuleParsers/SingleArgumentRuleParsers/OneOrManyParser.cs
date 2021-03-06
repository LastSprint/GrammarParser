﻿using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers {

    public class OneOrManyParser : SingleRuleParser {

        public const char Symbol = '+';

        protected override string TerminateSymbol => Symbol.ToString();

        public override IRule Parse(IParserImmutableContext context) {
            var result = this.TryParse(context);

            return result == null ? null : new OneOrManyRule(result);
        }

    }

}