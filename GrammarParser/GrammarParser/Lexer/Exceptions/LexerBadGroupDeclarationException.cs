using System;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Exceptions {

    public class LexerBadEndGroupDeclarationException: Exception {
        private readonly IParserImmutableContext _context;

        public override string Message => "Не удалось найти конец декларации группы" +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        public LexerBadEndGroupDeclarationException(IParserImmutableContext context) => this._context = context;
    }

    public class LexerBadStartGroupFeclarationException : Exception {

        private readonly IParserImmutableContext _context;

        public override string Message => "Не удалось найти начало декларации группы" +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        public LexerBadStartGroupFeclarationException(IParserImmutableContext context) => this._context = context;

    }

}