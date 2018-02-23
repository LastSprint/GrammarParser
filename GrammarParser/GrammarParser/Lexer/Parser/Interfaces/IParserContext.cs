using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Interfaces {

    /// <summary>
    /// Контекст парсера. Содержит информацию о текущем состоянии парсера.
    /// Неизменяемый.
    /// </summary>
    public interface IParserImmutableContext {

        /// <summary>
        /// Незименяемая очередь уже разобранных правил.
        /// </summary>
        IImmutableQueue<IRule> CurrentRuleCollection { get; }

        /// <summary>
        /// Поток разбираемых лексем.
        /// </summary>
        Stream CurrentStream { get; }
    }

    /// <summary>
    /// Контекст парсера. Содержит информацию о текущем состоянии парсера.
    /// Изменяемый.
    /// </summary>
    public interface IParserContext: IParserImmutableContext {

        /// <summary>
        /// Очередь уже разобранных правил.
        /// </summary>
        Queue<IRule> ParsedRules { get; set; }

    }

}
