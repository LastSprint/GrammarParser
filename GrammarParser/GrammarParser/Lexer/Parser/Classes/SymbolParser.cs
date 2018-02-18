// GrammarParser
// SymbolParser.cs
// Created 18.02.2018
// By Александр Кравченков

using System;
using System.IO;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Classes;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes {

    /// <summary>
    /// Пытается получить лексему символа: '?'
    /// Где ? это любой символ.
    /// </summary>
    public class SymbolParser: IParser {

        private const char StartDeclaration = '\'';

        private const char EndDeclaration = '\'';


        public IRule Parse(Stream stream) {

            var startPosition = stream.Position;
            var reader = new StreamReader(stream);

            var startDeclaration = reader.Read();
            var symbol = (char)reader.Read();
            var endDeclaration = reader.Read();

            reader.DiscardBufferedData();
            stream.Position = startPosition;

            if (startDeclaration != StartDeclaration) {
                return null;
            }

            if (symbol == StartDeclaration && symbol == EndDeclaration) {
                throw new SymbolParseEmptySymbolException();
            }

            if (endDeclaration != EndDeclaration) {
                throw new SymbolParserTooMuchSymbolsException(firstSymbol: symbol, secondSymbol: (char)endDeclaration);
            }

            stream.Position += 3;

            return new SymbolRule(symbol: symbol);
        }
    }
}
