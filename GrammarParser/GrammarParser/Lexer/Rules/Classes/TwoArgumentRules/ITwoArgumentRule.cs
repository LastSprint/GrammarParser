using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Rules.Classes.TwoArgumentRules {

    interface ITwoArgumentRule: IRule {

        IRule RightArgumentRule { get; }

        IRule LeftArgumentRule { get; }
    }
}