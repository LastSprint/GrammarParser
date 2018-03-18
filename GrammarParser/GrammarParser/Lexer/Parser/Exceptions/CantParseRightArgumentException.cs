using System;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Parser.Exceptions {

    public class CantParseRightArgumentException : Exception {

        private readonly IParserImmutableContext _context;

        private readonly string _ruleSymbol;

        public override string Message => $"Для правила \'{this._ruleSymbol}\' не удалось распарсить правый аргумент" +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        public CantParseRightArgumentException(string ruleSymbol, IParserImmutableContext context) {
            this._context = context;
            this._ruleSymbol = ruleSymbol;
        }
    }
}
