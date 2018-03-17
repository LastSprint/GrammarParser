using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using GrammarParser.Lexer.Exceptions;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Library;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer {

    /// <summary>
    /// Объет, который умеет разбирать поток символов в поток правил.
    /// То есть он умеет смтроить AST дерево по конкретной грамматике.
    /// </summary>
    public class Lexer: SingleRuleLexer {

        public Lexer(IParser parser, IBuilder<ILexer, Stream> selfBuilder): base(parser, selfBuilder) { }

        public override IParserContext Parse(Stream stream) {
            base.Context = new DefaultParserContext(stream: stream);
            var symbol = stream.CurrentSymbol();
            while (symbol != null) {

                switch (symbol) {

                    case SingleRuleLexer.EndGroup: return this.Context;
                    case SingleRuleLexer.StartGroup:

                        this.Context.CurrentStream.TryToSeekToNext();
                        var context = this.SelfBuilder.Build(this.Context.CurrentStream);
                        var result = context.Parse(this.Context.CurrentStream).ParsedRules.ToArray().Reverse();


                        if (this.Context.CurrentStream.CurrentSymbol() != SingleRuleLexer.EndGroup)
                        {
                            throw new LexerBadEndGroupDeclarationException(this.Context);
                        }

                        this.Context.CurrentStream.TryToSeekToNext();

                        var rule = new GroupRule(result.ToImmutableList());
                        this.Context.ParsedRules.Push(rule);

                        break;

                    default:
                        base.ParseCurrentSymbol(symbol);
                        break;

                }

                symbol = stream.CurrentSymbol();
            }
            return base.Context;
        }
    }
}