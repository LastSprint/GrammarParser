// GrammarParser
// IParser.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Interfaces {

    /// <summary>
    /// Интерфейс для абстрактного парсера лексем, который может:
    ///     - Попытаться распарсить и вернуть null в случае неудачи
    ///     - Попытаться распарсить и выкинуть исключение если лексема задекларирована с ошибкой (эти исключения нет смысла перехватывать - они дял пользователей. ДАльнейший парсинг в таком случае невозможен)
    ///     - Попытаться распарсить и вернуть <see cref="IRule"/>
    /// </summary>
    public interface IParser {
        IRule Parse(Stream stream);
    }

}