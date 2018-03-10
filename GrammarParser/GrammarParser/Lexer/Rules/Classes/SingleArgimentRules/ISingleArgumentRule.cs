using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Rules.Classes.SingleArgimentRules {

    public interface ISingleArgumentRule: IRule {
        IRule ArgumentRule { get; }
    }
}
