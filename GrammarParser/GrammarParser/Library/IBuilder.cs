namespace GrammarParser.Library {

    public interface IBuilder<Output, Input> {
        Output Build(Input arg);
    }
}
