// GrammarParser
// IRule.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Types.Interfaces {

    /// <summary>
    /// Абстрактное правило
    /// </summary>
    public interface IRule {

        /// <summary>
        /// Приоритет данного правила
        /// </summary>
        RulePriority Priority { get; }

        /// <summary>
        /// Проверяет поток входных символов на соответствие конкретному правилу.
        /// Причем, в случае, если результат проверки верный, то указатель потока сдвигается вперед на прочитанное кол-во элементов.
        /// В противном случае метод обязуется вернуть указатель в первоначальное состояние (каким оно было до того, как поток попал в метод)
        /// </summary>
        /// <param name="stream">Поток входных символов.</param>
        /// <returns>true - если поток соответствует правилу и false в противном случае</returns>
        bool Check(Stream stream);
    }

}