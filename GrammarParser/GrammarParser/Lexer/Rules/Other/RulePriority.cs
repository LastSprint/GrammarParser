// GrammarParser
// RulePriority.cs
// Created 18.02.2018
// By Александр Кравченков

using GrammarParser.Lexer.Types.Classes;

namespace GrammarParser.Lexer.Types.Other {

    /// <summary>
    /// Перечисление приоритетов для операторов лексера.
    /// </summary>
    public enum RulePriority: byte {

        /// <summary>
        ///  Правило "Или".
        /// </summary>
        RuleOr = 0,

        /// <summary>
        /// Правило группировки.
        /// </summary>
        RuleGrouping = 1,

        /// <summary>
        /// Правило "Один или ничего".
        /// <see cref="ZeroOnOneRule"/>
        /// </summary>
        RuleZeroOrOne = 2,

        /// <summary>
        /// Правило "Много или ничего".
        /// </summary>
        RuleZeroOrMany = 2,

        /// <summary>
        /// Правило "Один или много".
        /// </summary>
        RuleOneOrMany = 2,

        /// <summary>
        /// Правило отрезка.
        /// </summary>
        RuleRange = 3,

        /// <summary>
        /// Правило символа.
        /// <see cref="SymbolRule"/>
        /// </summary>
        RuleSymbol = 4
    }

}