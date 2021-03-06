﻿using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules {

    public class RangeRule : ITwoArgumentRule {

        private readonly SymbolRule _leftSymbolRule;

        private readonly SymbolRule _rightSymbolRule;

        public RangeRule(SymbolRule leftArgumentRule, SymbolRule rightArgumentRule) {
            this._rightSymbolRule = rightArgumentRule;
            this._leftSymbolRule = leftArgumentRule;
        }

        public IRule RightArgumentRule => this._rightSymbolRule;

        public IRule LeftArgumentRule => this._leftSymbolRule;

        public RulePriority Priority => RulePriority.RuleRange;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var readed = reader.Read();
            var result = readed >= this._leftSymbolRule.Symbol && readed <= this._rightSymbolRule.Symbol;
            reader.DiscardBufferedData();
            stream.Position = result ? startPosition + 1 : startPosition;
            this.ChekedString = ((char)readed).ToString();
            return result;
        }

    }

}