using System.Collections.Generic;
using System.IO;
using System.Linq;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes {

    public class GroupRule: IRule {

        public RulePriority Priority { get; }

        private readonly IReadOnlyList<IRule> _nestedRules;

        public GroupRule(IReadOnlyList<IRule> nestedRules) {
            this.Priority = RulePriority.RuleGrouping;
            this._nestedRules = nestedRules;
        }

        public bool Check(Stream stream) {

            foreach (var nestedRule in this._nestedRules) {
                if (!nestedRule.Check(stream)) {
                    return false;
                }
            }

            return true;
        }
    }
}
