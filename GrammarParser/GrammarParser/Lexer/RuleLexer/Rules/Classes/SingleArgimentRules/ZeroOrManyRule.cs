using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Lexer.Rules.Interfaces;

using ISingleArgumentRule = GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules.ISingleArgumentRule;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    public class ZeroOrManyRule : ISingleArgumentRule {

        public ZeroOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrMany;

        public bool Check(Stream stream) {
            while (this.ArgumentRule.Check(stream)) { }

            return true;
        }

    }

}