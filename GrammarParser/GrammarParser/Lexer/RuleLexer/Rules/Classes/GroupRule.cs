using System.Collections.Generic;
using System.IO;
using System.Text;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using static System.String;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes {

    public class GroupRule : IRule {

        public IReadOnlyList<IRule> NestedRules { get; }

        public GroupRule(IReadOnlyList<IRule> nestedRules) {
            this.Priority = RulePriority.RuleGrouping;
            this.NestedRules = nestedRules;
        }

        public RulePriority Priority { get; }

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            this.ChekedString = Empty;
            var builder = new StringBuilder();
            foreach (var nestedRule in this.NestedRules) {
                if (!nestedRule.Check(stream)) {
                    return false;
                }
                builder.Append(nestedRule.ChekedString);
            }

            this.ChekedString = builder.ToString();
            return true;
        }
    }
}