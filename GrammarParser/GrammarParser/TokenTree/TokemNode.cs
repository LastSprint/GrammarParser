﻿using System.Collections.Generic;

namespace GrammarParser.TokenTree {

    public class TokenNode: ITokenNode {

        public string Name { get; }
        public string Value { get; }

        public IList<ITokenNode> Childs { get; }

        public TokenNode(string name, string value, IList<ITokenNode> childs = null) {
            this.Name = name;
            this.Value = value;
            this.Childs = childs ?? new List<ITokenNode>();
        }

    }
}
