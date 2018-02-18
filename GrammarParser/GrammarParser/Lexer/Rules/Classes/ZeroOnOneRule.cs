// GrammarParser
// OneOrZeroRule.cs
// Created 18.02.2018
// By Александр Кравченков

using System.IO;

using GrammarParser.Lexer.Types.Interfaces;
using GrammarParser.Lexer.Types.Other;

namespace GrammarParser.Lexer.Types.Classes {

    public class ZeroOnOneRule: IRule {

        public RulePriority Priority => RulePriority.RuleZeroOrOne;

        public bool Check(Stream stream) {
            throw new System.NotImplementedException(); 
        }

    }

}