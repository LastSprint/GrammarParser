using GrammarParser.Lexer.RuleLexer.Rules.Classes;
using GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules;

namespace GrammarParser.Lexer.RuleLexer.Rules.Other {

    /// <summary>
    ///     Перечисление приоритетов для операторов лексера.
    /// </summary>
    public enum RulePriority : byte {

        /// <summary>
        ///     Правило "Или".
        /// </summary>
        RuleOr = 0,

        /// <summary>
        ///     Правило группировки.
        /// </summary>
        RuleGrouping = 2,

        /// <summary>
        ///     Правило "Один или ничего".
        ///     <see cref="OneOrZeroRule" />
        /// </summary>
        RuleZeroOrOne = 1,

        /// <summary>
        ///     Правило "Много или ничего".
        /// </summary>
        RuleZeroOrMany = 1,

        /// <summary>
        ///     Правило "Один или много".
        /// </summary>
        RuleOneOrMany = 1,

        /// <summary>
        ///     Правило отрезка.
        /// </summary>
        RuleRange = 3,

        /// <summary>
        ///     Правило символа.
        ///     <see cref="SymbolRule" />
        /// </summary>
        RuleSymbol = 4,

        /// <summary>
        ///     Для пльзовательского правила:
        ///     <see cref="UserRule"/>
        /// </summary>
        UserRule = 4

    }

}