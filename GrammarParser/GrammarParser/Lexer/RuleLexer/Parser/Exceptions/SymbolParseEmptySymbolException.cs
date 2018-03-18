using System;

namespace GrammarParser.Lexer.Parser.Exceptions {

    public class SymbolParseEmptySymbolException : Exception {

        public override string Message =>
            "Вы пытаетесь задать пустой символ - ''. Внутри оператора ' ' должен быть один символ!";

    }

}