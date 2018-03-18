using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

using ISingleArgumentRule = GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules.ISingleArgumentRule;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules {

    public class OneOrManyRule : ISingleArgumentRule {

        public OneOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleOneOrMany;

        public bool Check(Stream stream) {
            if (!this.ArgumentRule.Check(stream)) {
                // Правило не выполнилось - все норм
                return false;
            }

            while (this.ArgumentRule.Check(stream)) { }

            return true;
        }

    }

}