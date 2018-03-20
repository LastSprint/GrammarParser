// GrammarParser
// SymbolRule.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Other;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes {

    /// <summary>
    ///     Правило для определения одного символа. Имеет вид: '$any_utf_symbol$'
    /// </summary>
    public class SymbolRule : IRule {

        public readonly char Symbol;

        public SymbolRule(char symbol) => this.Symbol = symbol;

        public RulePriority Priority => RulePriority.RuleSymbol;

        public string ChekedString { get; private set; }

        public bool Check(Stream stream) {
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var result = reader.Read() == this.Symbol;
            reader.DiscardBufferedData();
            stream.Position = result ? startPosition + 1 : startPosition;
            this.ChekedString = result ? this.Symbol.ToString() : string.Empty;
            return result;
        }

    }

}