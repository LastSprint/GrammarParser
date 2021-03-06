﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

using GrammarParser.AstTree.Interfaces;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Configurations;
using GrammarParser.Lexer.RuleLexer.Rules.Other;
using GrammarParser.Lexer.StructureLexer.Models;
using GrammarParser.Library.Extensions;
using GrammarParser.TokenTree;
using static System.String;

namespace GrammarParser.Lexer.StructureLexer.Rules {

    public class UserRule: IStructureRule {

        public static char TokenExpressionDivider = ',';

        public RulePriority Priority { get; }

        public string ChekedString { get; private set; }

        public string Name { get; }
        public string RulePattern { get; }
        public TokenExpression TokenConvertionPattern { get; }

        private IAstTree _tree;

        public UserRule(string name, string rulePattern, TokenExpression tokenConvertionPattern) {
            this.Priority = RulePriority.UserRule;

            this.Name = name;
            this.RulePattern = rulePattern;
            this.TokenConvertionPattern = tokenConvertionPattern;
        }

        public bool IsValid(IParserImmutableContext context) {

            var stream = new MemoryStream().FromString(this.RulePattern);
            var lexer = LexerBuilder.DefaultAstLexer;
            var tree = new AstTree.AstTree(lexer.Parse(stream));
            
            this._tree = tree;

            return this.TokenConvertionPattern.Childs.Values.Max() <= tree.NodeCount;
        }

        public bool Check(Stream stream) {

            this.ChekedString = Empty;
            if (this._tree == null) {
                var ruleStream = new MemoryStream().FromString(this.RulePattern);
                this._tree = new AstTree.AstTree(LexerBuilder.DefaultAstLexer.Parse(ruleStream));
            }

            var result = false;
            try {
                result = this._tree.Check(stream);
            }
            catch {
                result = false;
            }

            this.ChekedString = this._tree.ParsedResult;
            return result;
        }

        public ITokenNode Convert() {
            var list = new List<IAstNode>();
            this._tree.DeepWalk(x => list.Add(x));

            var childs = new List<ITokenNode>();

            foreach (var child in this.TokenConvertionPattern.Childs) {
                childs.Add(new TokenNode(child.Key, list[child.Value].ParsedResult));
            }

            return new TokenNode(this.TokenConvertionPattern.Name, null, childs);
        }

    }
}
