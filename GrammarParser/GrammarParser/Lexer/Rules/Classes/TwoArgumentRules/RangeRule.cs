using System.IO;

using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes.TwoArgumentRules {
    
    public class RangeRule: ITwoArgumentRule {

        public IRule RightArgumentRule => this._rightSymbolRule;

        public IRule LeftArgumentRule => this._leftSymbolRule;


        private SymbolRule _rightSymbolRule;

        private SymbolRule _leftSymbolRule;

        public RulePriority Priority => RulePriority.RuleRange;

        public RangeRule(SymbolRule leftArgumentRule, SymbolRule rightArgumentRule) {
            this._rightSymbolRule = rightArgumentRule;
            this._leftSymbolRule = leftArgumentRule;
        }

        public bool Check(Stream stream) {

            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var readed = reader.Read();
            var result = readed >= this._leftSymbolRule.Symbol && readed <= this._rightSymbolRule.Symbol;
            reader.DiscardBufferedData();
            stream.Position = result ? startPosition + 1 : startPosition;
            return result;
        }
    }
}
