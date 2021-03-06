﻿using System;

using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Parser.Exceptions {

    public class BadArgumentInRangeRuleException : Exception {

        public override string Message => $"Для правила \'..\' аргументами должны быть символы" +
                                          $"{Environment.NewLine}Контекст: {this._context}";

        private readonly IParserImmutableContext _context;

        public BadArgumentInRangeRuleException(IParserImmutableContext context) => this._context = context;

    }

}