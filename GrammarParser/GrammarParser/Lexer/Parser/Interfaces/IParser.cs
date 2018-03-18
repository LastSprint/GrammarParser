// GrammarParser
// IParser.cs
// Created 18.02.2018
// By Александр Кравченков

using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Interfaces {

    /// <summary>
    /// Интерфейс для абстрактного парсера лексем, который может:
    ///     - Попытаться распарсить и вернуть null в случае неудачи
    ///     - Попытаться распарсить и выкинуть исключение если лексема задекларирована с ошибкой (эти исключения нет смысла перехватывать - они дял пользователей. ДАльнейший парсинг в таком случае невозможен)
    ///     - Попытаться распарсить и вернуть <see cref="IRule"/>
    /// </summary>
    public interface IParser {

        /// <summary>
        /// Проверяет, возможно ли из конкретного состояния контекста распарсить данное правило.
        /// Не должен кидать исключения, поскольку по сути не является методом парсинга.
        /// (Так как у нас тут не Nullsafety то с определенной долей везения можно утверждать, что IParser.Parse() вернет null, если распарсить не удалось.
        /// Но я бы не полагался на это (:
        /// </summary>
        /// <param name="context">Контекст парсера-предка.</param>
        /// <returns>true если парсинг возможен и false в обратном случае</returns>
        bool IsCurrentRule(IParserImmutableContext context);

        /// <summary>
        /// Вызывает парсинг соответствующего аргумента.
        /// </summary>
        /// <param name="context">Контекст, парсера-предка.</param>
        /// <returns>Правило</returns>
        IRule Parse(IParserImmutableContext context);
    }

}