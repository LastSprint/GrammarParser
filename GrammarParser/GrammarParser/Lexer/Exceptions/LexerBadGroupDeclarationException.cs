using System;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Exceptions {

    public class LexerBadGroupDeclarationException: Exception {
        private readonly IParserImmutableContext _context;

        public override string Message => $"Не удалось найти конец дкларации группы" +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        public LexerBadGroupDeclarationException(IParserImmutableContext context) => this._context = context;
    }
}