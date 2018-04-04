using System.IO;
using System.Text;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.StructureLexer.Parsers {

    /// <summary>
    ///  Предполагается получить контент структуры
    /// </summary>
    public class UserRuleStructureParser {

        private UserRuleParser _parser;
        private IParserContext _context;

        public UserRuleStructureParser(UserRuleParser parser) {
            this._parser = parser;
        }

        //IList<UserRule> Parse(Stream stream) {
        //    this._context = new DefaultParserContext(stream: stream );
        //    var poition = stream.Position;
        //    var builder = new StringBuilder();
        //    var rules = new List<UserRule>();
        //    var rule = this.ReadRule(stream);

        //    while (rule != null) {
        //        var parsed = this._parser.Parse(this._context) as UserRule;
        //        this._context.ParsedRules.Push(parsed);
        //        rule = this.ReadRule(stream);
        //    }
        //}

        private string ReadRule(Stream stream) {
            var position = stream.Position;
            var builder = new StringBuilder();

            var reader = new StreamReader(stream);

            char current;
            var next = (char)reader.Read();

            do {

                if (reader.EndOfStream) {
                    reader.DiscardBufferedData();
                    stream.Position = position;

                    return null;
                }

                current = next;
                next = (char)reader.Read();
                builder.Append(current);
            } while (current != ';' && next != '\'');

            stream.Position = position + builder.Length;

            return builder.ToString();
        }
    }
}
