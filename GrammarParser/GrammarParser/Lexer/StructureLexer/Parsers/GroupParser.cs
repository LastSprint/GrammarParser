using System;
using System.IO;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer.StructureLexer.Parsers {

    public class GroupParser {

        private DefaultParserContext _context;

        private IParserContext _parser;


        public IParserContext Parse(Stream stream) {
            this._context = new DefaultParserContext(stream);


            var rule = "Rule";

            while (stream.NextWithSkipedEmpty() != null) {

                var name = this.ReadBlockMeta(stream);

                if (name == rule) {
                    var rules = new UserRuleStructureParser(new UserRuleParser()).Parse(this._context);
                    foreach (var userRule in rules) {
                        this._context.ParsedRules.Push(userRule);
                    }
                }
                if (name == null) {
                    return this._context;
                }
            }
            return this._context;
        }

        private string ReadBlockMeta(Stream stream) {
            var block = "block";
            var startSymbol = stream.NextWithSkipedEmpty();
            if (!startSymbol.HasValue) {
                return null;
            }

            for (var i = 0; i < block.Length; i++) {
                if (stream.CurrentSymbol() != block[i]) {
                    throw new Exception();
                }
                stream.TryToSeekToNext();
            }

            startSymbol = stream.NextWithSkipedEmpty();
            if (!startSymbol.HasValue) {
                return null;
            }

            var name = "";

            while (startSymbol != '{' && startSymbol != null) {
                name += startSymbol;
                stream.TryToSeekToNext();
                startSymbol = stream.CurrentSymbol();
            }

            return name.Trim();
        }

    }

}
