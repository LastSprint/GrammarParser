using System.IO;

using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes.TwoArgumentRules {

    public class DisjunctionRule: ITwoArgumentRule {

        public IRule RightArgumentRule { get; }

        public IRule LeftArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleOr;

        public DisjunctionRule(IRule leftArgumentRule, IRule rightArgumentRule) {
            this.LeftArgumentRule = leftArgumentRule;
            this.RightArgumentRule = rightArgumentRule;
        }

        public bool Check(Stream stream)=> this.LeftArgumentRule.Check(stream) || this.RightArgumentRule.Check(stream);
    }
}
