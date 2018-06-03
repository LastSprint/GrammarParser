using System.Collections.Generic;
using System.IO;
using System.Text;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.StructureLexer.Rules;
using GrammarParser.Library.Extensions;

namespace GrammarParser.Lexer.StructureLexer.Parsers {

    /// <summary>
    ///  Предполагается получить контекст структуры
    /// </summary>
    public class UserRuleStructureParser {

        public const string BlockName = "Rules";

        private UserRuleParser _parser;
        private IParserImmutableContext _context;

        public UserRuleStructureParser(UserRuleParser parser) {
            this._parser = parser;
        }

        public IList<UserRule> Parse(IParserImmutableContext context) {
            this._context = context;
            var stream = context.CurrentStream;
            var poition = stream.Position;
            var rules = new List<UserRule>();

            try {
                while(true) {
                    var pos = stream.Position;

                    if (stream.NextWithSkipedEmpty() == '}') {
                        stream.TryToSeekToNext();
                        return rules;
                    }

                    //stream.Position = pos;

                    var parsed = this._parser.Parse(this._context) as UserRule;
                    rules.Add(parsed);
                }

            }
            catch {
                stream.Position = poition;
                 throw;
            }
        }

        private string ReadRule(Stream stream) {
            var position = stream.Position;
            var builder = new StringBuilder();

            var reader = new StreamReader(stream);

            char current;
            var next = (char) reader.Peek();

            do {
                current = (char)reader.Read();
                next = (char)reader.Peek();

                
                if (current == UserRuleParser.RuleEndTerminator)
                {
                    if (next != '\'')
                    {
                        break;
                    }
                }

                builder.Append(current);

                if (reader.EndOfStream)
                {
                    reader.DiscardBufferedData();
                    stream.Position = position;

                    return null;
                }
            } while (true);

            stream.Position = position + builder.Length;

            return builder.ToString();
        }
    }
}
