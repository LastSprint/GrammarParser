using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    public interface ISingleArgumentRule: IRule {
        IRule ArgumentRule { get; }
    }
}
