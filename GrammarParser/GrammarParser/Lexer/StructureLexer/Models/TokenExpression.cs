using System.Collections.Generic;

namespace GrammarParser.Lexer.StructureLexer.Models {

    public class TokenExpression {

        public string Name { get; }

        public IDictionary<string, int> Childs { get; }

        public TokenExpression(string name, IDictionary<string, int> childs) {
            this.Name = name;
            this.Childs = childs;
        }
    }
}
