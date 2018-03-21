using System;

namespace GrammarParser.AstTree.Interfaces {

    public interface IAstTree: IAstNode {

        IAstNode Root { get; }

        void DeepWalk(Action<IAstNode> action);
    }

}