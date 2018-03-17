﻿using System.IO;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Configurations {

    public class LexerBuilder: IBuilder<ILexer, Stream> {

        private readonly IInjector<IParser> _parserInjector;
        
        public LexerBuilder(IInjector<IParser> parserInjector) => this._parserInjector = parserInjector;

        public ILexer Build(Stream arg) => new Lexer(this._parserInjector.Injection(), this);

    }
}