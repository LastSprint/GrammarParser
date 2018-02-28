using System.IO;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer {

    /// <summary>
    /// Интерфейс для объекта лексера
    /// В моем мире это объект, который из потока лексем делает AST дерево.
    /// </summary>
    public interface ILexer {
        IParserContext Parse(Stream stream);
    }
}
