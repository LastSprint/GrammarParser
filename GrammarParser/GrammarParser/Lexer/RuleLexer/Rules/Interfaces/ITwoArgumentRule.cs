using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Rules.Interfaces {

    public interface ITwoArgumentRule : IRule {

        IRule LeftArgument { get; }

        IRule RightArgument { get; }

    }

}