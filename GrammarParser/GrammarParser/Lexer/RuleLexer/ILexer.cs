using System.IO;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer {

    /// <summary>
    ///     Интерфейс для объекта лексера
    ///     В моем мире это объект, который из потока лексем делает AST дерево.
    /// </summary>
    public interface ILexer {

        IParserContext Parse(Stream stream);

        /// <summary>
        ///     Парсит только одно следующее правило.
        /// </summary>
        IRule ParseNextRule(Stream stream);

    }

}