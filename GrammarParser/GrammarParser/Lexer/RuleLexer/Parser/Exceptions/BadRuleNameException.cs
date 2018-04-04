using System;
using System.Collections.Generic;
using System.Text;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Parser.Exceptions {

    public class BadRuleNameException: Exception {

        private readonly string _name;
        private readonly IParserImmutableContext _context;

        public override string Message => $"Не удалось найти правило с именем {this._name}.{Environment.NewLine}Контекст: {this._context}";

        public BadRuleNameException(string name, IParserImmutableContext context) {
            this._context = context;
            this._name = name;
        }
    }
}
