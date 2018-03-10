using System.IO;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    public class ZeroOrManyRule: ISingleArgumentRule {
        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrMany;

        public ZeroOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public bool Check(Stream stream) {

            while (this.ArgumentRule.Check(stream)) { }

            return true;
        }
    }
}
