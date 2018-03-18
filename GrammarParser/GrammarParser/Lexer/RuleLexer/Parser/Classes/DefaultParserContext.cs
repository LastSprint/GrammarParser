﻿using System;
using System.Collections.Generic;
using System.IO;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Parser.Classes {

    public class DefaultParserContext : IParserContext {

        public DefaultParserContext(Stream stream) {
            this.CurrentStream = stream;
            this.ParsedRules = new Stack<IRule>();
        }

        public IBuilder<ILexer, Stream> LexerBuilder { get; set; }

        public IReadOnlyCollection<IRule> CurrentRuleCollection => this.ParsedRules;

        public Stack<IRule> ParsedRules { get; set; }

        public IRule Pop() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Pop();

        public IRule Peek() => this.ParsedRules.Count == 0 ? null : this.ParsedRules.Peek();

        public Stream CurrentStream { get; }

        public void Merge(IParserContext context) {
            this.CurrentStream.Position = context.CurrentStream.Position;
        }

        public override string ToString() {
            var result = $"Stream position: {this.CurrentStream.Position}";
            result += $"{Environment.NewLine}Context:";
            foreach (var parsedRule in this.ParsedRules) {
                result += $"{Environment.NewLine}{parsedRule}";
            }

            return result;
        }

    }

}