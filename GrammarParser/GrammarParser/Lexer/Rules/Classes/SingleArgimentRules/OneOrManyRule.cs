using System.IO;

using GrammarParser.Lexer.Types.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    public class OneOrManyRule: ISingleArgumentRule {

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleOneOrMany;

        public OneOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public bool Check(Stream stream) {

            if (!this.ArgumentRule.Check(stream))
            {
                // Правило не выполнилось - все норм
                return false;
            }

            while (this.ArgumentRule.Check(stream)) { }

            return true;
        }

    }
}
