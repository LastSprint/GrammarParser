using System;
using System.IO;
using System.Linq;
using System.Text;

using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Exceptions;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.StructureLexer.Rules;

namespace GrammarParser.Lexer.RuleLexer.Parser.Classes {

    public class RuleCallParser: IParser {

        public bool IsCurrentRule(IParserImmutableContext context) {
             var name = this.ReadRuleName(context.CurrentStream);
            return DefaultParserContext.GlobalContext.UserRules.FirstOrDefault(x => x.Name == name) != default(UserRule);
            //return context.UserRules.FirstOrDefault(x => x.Name == name) != default(UserRule);
        }

        public IRule Parse(IParserImmutableContext context) {
            var name = this.ReadRuleName(context.CurrentStream);
            var rule = DefaultParserContext.GlobalContext.UserRules.FirstOrDefault(x => x.Name == name) ?? context.UserRules.FirstOrDefault(x => x.Name == name);
            if (rule == null) {
                throw new BadRuleNameException(name, context);
            }
            context.CurrentStream.Position += name.Length;
            return rule;
        }

        private string ReadRuleName(Stream stream) {
            var startPos = stream.Position;

            var reader = new StreamReader(stream);

            var symbol = (char) reader.Read();

            var builer = new StringBuilder();

            while (CharacterSet.AllLettersAndNumbers.Contains(symbol)) {
                builer.Append(symbol);
                symbol = (char) reader.Read();
            }

            reader.DiscardBufferedData();
            stream.Position = startPos;

            return builer.ToString();
        }
    }
}
