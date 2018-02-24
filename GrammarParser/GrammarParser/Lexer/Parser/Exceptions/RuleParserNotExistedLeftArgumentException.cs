using System;

using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Parser.Exceptions {

    public class RuleParserNotExistedLeftArgumentException: Exception {

        private readonly IParserImmutableContext _context;

        private readonly string _ruleSymbol;

        public override string Message => $"Не удалось распарсить правило: {this._ruleSymbol} так как девый аргумент не удалось распарсить левый аргумент." +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        public RuleParserNotExistedLeftArgumentException(IParserImmutableContext context, string ruleSymbol) {
            this._context = context;
            this._ruleSymbol = ruleSymbol;
        }
    }
}
