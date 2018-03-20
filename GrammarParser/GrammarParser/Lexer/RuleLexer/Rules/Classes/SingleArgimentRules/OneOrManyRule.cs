using System.IO;
using System.Text;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;


namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules {

    public class OneOrManyRule: ISingleArgumentRule {

        public OneOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleOneOrMany;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            if (!this.ArgumentRule.Check(stream)) {
                // Правило не выполнилось - беда
                return false;
            }

            var builder = new StringBuilder(this.ArgumentRule.ChekedString);

            while (this.ArgumentRule.Check(stream)) {
                builder.Append(this.ArgumentRule.ChekedString);
            }

            this.ChekedString = builder.ToString();

            return true;
        }

    }

}