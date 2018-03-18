using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;

namespace GrammarParser.Lexer.RuleLexer.Rules.Classes.SingleArgimentRules {

    public interface ISingleArgumentRule : IRule {

        IRule ArgumentRule { get; }

    }

}