using System.IO;

using GrammarParser.Lexer.Types.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    /// <summary>
    /// Вложенное правило выполняется либо один раз либо не выполняется
    /// </summary>
    public class OneOrZeroRule: ISingleArgumentRule {
        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrOne;

        public OneOrZeroRule(IRule argument) => this.ArgumentRule = argument;

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
