using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Rules.Interfaces {

    public interface ITwoArgumentRule: IRule {

        IRule LeftArgument { get; }

        IRule RightArgument { get; }

    }
}
