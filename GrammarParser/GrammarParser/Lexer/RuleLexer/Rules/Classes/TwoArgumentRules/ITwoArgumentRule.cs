using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.TwoArgumentRules {

    internal interface ITwoArgumentRule : IRule {

        IRule RightArgumentRule { get; }

        IRule LeftArgumentRule { get; }

    }

}