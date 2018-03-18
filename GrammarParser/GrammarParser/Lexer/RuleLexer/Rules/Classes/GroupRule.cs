using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Rules.Classes {

    public class GroupRule : IRule {

        public IReadOnlyList<IRule> NestedRules { get; }

        public GroupRule(IReadOnlyList<IRule> nestedRules) {
            this.Priority = RulePriority.RuleGrouping;
            this.NestedRules = nestedRules;
        }

        public RulePriority Priority { get; }

        public bool Check(Stream stream) {
            foreach (var nestedRule in this.NestedRules) {
                if (!nestedRule.Check(stream)) {
                    return false;
                }
            }

            return true;
        }

    }

}