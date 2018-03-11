using System.Collections.Generic;
using System.IO;

namespace GrammarParser.AstTree.Interfaces {

    public interface IAstNode {

        IList<IAstNode> Childs { get; }

        /// <summary>
        /// Проверяет поток на удовлетворение этому узлу дерева и сдвигает указатель чтения потока на нужное кол-во символов. 
        /// В противном случае возвращает false.
        /// </summary>
        /// <param name="stream">Поток символов</param>
        /// <returns>true - если символы подходят данному AST узлу</returns>
        bool Check(Stream stream);
    }
}
