using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Lexer.Rules.Interfaces;

using ISingleArgumentRule = GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules.ISingleArgumentRule;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    /// <summary>
    ///     Вложенное правило выполняется либо один раз либо не выполняется
    /// </summary>
    public class OneOrZeroRule : ISingleArgumentRule {

        public OneOrZeroRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrOne;

        public bool Check(Stream stream) {
            var streamStartPosition = stream.Position;

            if (!this.ArgumentRule.Check(stream)) {
                // Правило не выполнилось - все норм
                return true;
            }

            if (!this.ArgumentRule.Check(stream)) {
                // Правило отработало только один раз - все норм
                return true;
            }

            stream.Position = streamStartPosition;
            return false;
        }

    }

}