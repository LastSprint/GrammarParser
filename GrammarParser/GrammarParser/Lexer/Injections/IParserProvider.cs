using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Injections {

    public interface IParserProvider {

        IParser NonArgumentParser { get; }
        IParser OneArgumentParer { get; }
        IParser TwoArgumentParser { get; }
    }
}
