using System.IO;
using System.Text;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules {

    public class ZeroOrManyRule : ISingleArgumentRule {

        public ZeroOrManyRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrMany;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            var builder = new StringBuilder(string.Empty);
            while (this.ArgumentRule.Check(stream)) {
                builder.Append(this.ArgumentRule.ChekedString);
            }

            this.ChekedString = builder.ToString();
            return true;
        }

    }

}