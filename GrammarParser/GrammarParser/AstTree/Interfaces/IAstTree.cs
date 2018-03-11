
namespace GrammarParser.AstTree.Interfaces {

    public interface IAstTree: IAstNode {

        IAstNode Root { get; }
    }
}
