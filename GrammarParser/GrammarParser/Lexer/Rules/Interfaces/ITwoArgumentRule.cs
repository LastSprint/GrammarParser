// GrammarParser
// ISingleArgumentRule.cs
// Created 18.02.2018
// By Александр Кравченков

namespace GrammarParser.Lexer.Types.Interfaces {

    /// <summary>
    /// Представлет собой усложненное правило, которое имет явный аргумент - другое правило.
    /// </summary>
    public interface ISingleArgumentRule: IRule {

        /// <summary>
        /// Зависимое правило.
        /// То есть, сначала высчитывается `LeftArgument` а затем уже вычисляется правило контейнера.
        /// </summary>
        IRule LeftArgument { get; set; }
    }

}