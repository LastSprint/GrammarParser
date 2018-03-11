// GrammarParser
// SymbolRule.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Rules.Classes {

    /// <summary>
    /// Правило для определения одного символа. Имеет вид: '$any_utf_symbol$'
    /// </summary>
    public class SymbolRule: IRule {

        public readonly char Symbol;

        public RulePriority Priority => RulePriority.RuleSymbol;

        public SymbolRule(char symbol) => this.Symbol = symbol;

        public bool Check(Stream stream) {
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var result = reader.Read() == this.Symbol;
            reader.DiscardBufferedData();
            stream.Position = result ? startPosition + 1 : startPosition;
            return result;
        }
    }
}