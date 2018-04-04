using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Parser.Interfaces {

    /// <summary>
    ///     Контекст парсера. Содержит информацию о текущем состоянии парсера.
    ///     Неизменяемый.
    /// </summary>
    public interface IParserImmutableContext {

        IBuilder<ILexer, Stream> LexerBuilder { get; }

        /// <summary>
        ///     Незименяемая коллекция уже разобранных правил.
        /// </summary>
        IReadOnlyCollection<IRule> CurrentRuleCollection { get; }

        IReadOnlyCollection<UserRule> UserRules { get; }

        /// <summary>
        ///     Поток разбираемых лексем.
        /// </summary>
        Stream CurrentStream { get; }

        /// <summary>
        ///     Возвращает последний элемент в стеке (предварительно удаляя его в самом контексте)
        ///     Этот метод немного нарушает иммутабельность, но в необходимых пределах.
        ///     В случае, если стек пуст вернет null
        /// </summary>
        IRule Pop();

        /// <summary>
        ///     <see cref="Stack{T}.Peek" /> только этот вернет null если пуcто.
        /// </summary>
        IRule Peek();
    }

    /// <summary>
    ///     Контекст парсера. Содержит информацию о текущем состоянии парсера.
    ///     Изменяемый.
    /// </summary>
    public interface IParserContext : IParserImmutableContext {

        /// <summary>
        ///     Коллекция уже разобранных правил.
        /// </summary>
        Stack<IRule> ParsedRules { get; set; }

    }

}