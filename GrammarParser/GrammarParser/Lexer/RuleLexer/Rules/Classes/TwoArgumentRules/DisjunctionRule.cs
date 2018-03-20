using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules {

    public class DisjunctionRule : ITwoArgumentRule {

        public DisjunctionRule(IRule leftArgumentRule, IRule rightArgumentRule) {
            this.LeftArgumentRule = leftArgumentRule;
            this.RightArgumentRule = rightArgumentRule;
        }

        public IRule RightArgumentRule { get; }

        public IRule LeftArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleOr;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {

            if (this.LeftArgumentRule.Check(stream)) {
                this.ChekedString = this.LeftArgumentRule.ChekedString;
                return true;
            } else if (this.RightArgumentRule.Check(stream)) {
                this.ChekedString = this.RightArgumentRule.ChekedString;
                return true;
            }

            return false;
        }

    }

}