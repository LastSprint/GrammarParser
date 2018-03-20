using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.TokenTree;

namespace GrammarParser.Lexer.StructureLexer.Rules {

    public interface ITokenConvertionable {
        ITokenNode Convert();
    }

    public interface IStructureRule : IRule, ITokenConvertionable { }

}
