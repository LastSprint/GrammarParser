using System;

namespace GrammarParser.Lexer.StructureLexer.Parsers.Exceptions {

    public class UserRuleParserBadNameException : Exception {
        public override string Message => "Ну удалось распарсить имя пользовательского правила";
    }

    public class UserRuleParserBadRulePatternException : Exception {
        public override string Message => "Ну удалось распарсить паттерн пользовательского правила";
    }

    public class UserRuleParserBadTokenExpressionException : Exception {
        public override string Message => "Ну удалось распарсить конвертирование в токены пользовательского правила";
    }
}
