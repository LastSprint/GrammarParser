using System;

namespace GrammarParser.Lexer.Parser.Exceptions {

    public class SymbolParserTooMuchSymbolsException : Exception {

        public override string Message =>
            $"Вы пытаетесь декларировать строку с помощью символьного оператора: '{this._firstSymbol}{this._secondSymbol}...";

        private readonly char _firstSymbol;

        private readonly char _secondSymbol;

        public SymbolParserTooMuchSymbolsException(char firstSymbol, char secondSymbol) {
            this._firstSymbol = firstSymbol;
            this._secondSymbol = secondSymbol;
        }

    }

}