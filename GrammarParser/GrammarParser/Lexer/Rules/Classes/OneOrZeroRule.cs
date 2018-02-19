using System.IO;
using GrammarParser.Lexer.Types.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes {

    /// <summary>
    /// Вложенное правило выполняется либо один раз либо не выполняется
    /// </summary>
    public class OneOrZeroRule: IRule {

        private IRule _argumentRule;

        public RulePriority Priority => RulePriority.RuleZeroOrOne;

        public OneOrZeroRule(IRule argument) {
            this._argumentRule = argument;
        }

        public bool Check(Stream stream) {

            var streamStartPosition = stream.Position;

            if (!this._argumentRule.Check(stream)) {
                // Правило не выполнилось - все норм
                return true;
            }

            if (!this._argumentRule.Check(stream)) {
                // Правило отработало только один раз - все норм
                return true;
            }

            stream.Position = streamStartPosition;
            return false;
        }
    }
}
