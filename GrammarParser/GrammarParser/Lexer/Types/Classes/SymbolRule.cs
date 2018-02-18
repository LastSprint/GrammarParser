// GrammarParser
// SymbolRule.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.Types.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Types.Classes {

    /// <summary>
    /// Правило для определения одного символа. Имеет вид: '$any_utf_symbol$'
    /// </summary>
    public class SymbolRule: IRule {

        private readonly char _symbol;

        public RulePriority Priority => RulePriority.RuleSymbol;

        public SymbolRule(char symbol) => this._symbol = symbol;

        public bool Check(Stream stream) {
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var result = reader.Read() == this._symbol;
            reader.DiscardBufferedData();
            stream.Position = result ? startPosition + 1 : startPosition;
            return result;
        }

    }
}