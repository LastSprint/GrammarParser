using System.IO;
using System.Text;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules {

    /// <summary>
    ///     Вложенное правило выполняется либо один раз либо не выполняется
    /// </summary>
    public class OneOrZeroRule : ISingleArgumentRule {

        public OneOrZeroRule(IRule argument) => this.ArgumentRule = argument;

        public IRule ArgumentRule { get; }

        public RulePriority Priority => RulePriority.RuleZeroOrOne;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            var streamStartPosition = stream.Position;
            
            if (!this.ArgumentRule.Check(stream)) {
                // Правило не выполнилось - все норм
                this.ChekedString = "";
                return true;
            }
            var builder = new StringBuilder(this.ArgumentRule.ChekedString);
            if (!this.ArgumentRule.Check(stream)) {
                // Правило отработало только один раз - все норм
                builder.Append(this.ArgumentRule.ChekedString);
                this.ChekedString = builder.ToString();
                return true;
            }

            stream.Position = streamStartPosition;
            return false;
        }

    }

}