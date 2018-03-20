// GrammarParser
// SymbolParser.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Parser.Classes {

    /// <summary>
    ///     Пытается получить лексему символа: '?'
    ///     Где ? это любой символ.
    /// </summary>
    public class SymbolParser : IParser {

        private const char StartDeclaration = '\'';

        private const char EndDeclaration = '\'';

        public bool IsCurrentRule(IParserImmutableContext context) {
            var stream = context.CurrentStream;
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);

            var startDeclaration = reader.Read();
            var symbol = (char) reader.Read();
            var endDeclaration = reader.Read();

            reader.DiscardBufferedData();
            stream.Position = startPosition;


            if (startDeclaration != StartDeclaration) {
                return false;
            }

            if (symbol == StartDeclaration && symbol == EndDeclaration) {
                return false;
            }

            return endDeclaration == EndDeclaration;
        }

        public IRule Parse(IParserImmutableContext context) {
            var stream = context.CurrentStream;
            var startPosition = stream.Position;
            var reader = new StreamReader(stream);

            var startDeclaration = reader.Read();
            var symbol = (char) reader.Read();
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
                throw new SymbolParserTooMuchSymbolsException(symbol, (char) endDeclaration);
            }

            stream.Position += 3;

            return new SymbolRule(symbol);
        }
    }
}