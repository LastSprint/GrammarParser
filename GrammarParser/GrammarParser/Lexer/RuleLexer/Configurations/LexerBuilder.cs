using System.IO;

using GrammarParser.Lexer;
using GrammarParser.Lexer.Injections.Injectors;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.RuleLexer.Configurations {

    public class LexerBuilder : IBuilder<ILexer, Stream> {

        public static ILexer DefaultAstLexer => new Lexer(new SimpleParserInjector().Injection(), new LexerBuilder(new SimpleParserInjector()));

        private readonly IInjector<IParser> _parserInjector;

        public LexerBuilder(IInjector<IParser> parserInjector) => this._parserInjector = parserInjector;

        public ILexer Build(Stream arg) => new Lexer(this._parserInjector.Injection(), this);

    }

}