using System;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Classes;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers {

    public class OneOrZeroParser: IParser {

        public const char Symbol = '?';

        public bool IsCurrentRule(IParserImmutableContext context) {
            var(argument, symbol) = this.ProcessContext(context: context);

            return symbol == OneOrZeroParser.Symbol && argument != null;
        }

        public IRule Parse(IParserImmutableContext context) {

            var (argument, symbol) = this.ProcessContext(context: context);

            if (symbol != OneOrZeroParser.Symbol) {
                return null;
            }

            if (argument == null) {
                throw new RuleParserNotExistedLeftArgumentException(context: context,
                    ruleSymbol: $"{OneOrZeroParser.Symbol}");
            }

            context.CurrentStream.Position += 1;

            return new OneOrZeroRule(argument);
        }

        private (IRule leftArgument, char symbol) ProcessContext(IParserImmutableContext context) {
            var startStreamPosition = context.CurrentStream.Position;
            var reader = new StreamReader(context.CurrentStream);
            var symbol = (char)reader.Read();
            IRule leftArgument;
            try {
                leftArgument = context.CurrentRuleCollection.First();
            }
            catch (InvalidOperationException) {
                throw new RuleParserNotExistedLeftArgumentException(context: context, ruleSymbol: OneOrZeroParser.Symbol.ToString());
            }

            reader.DiscardBufferedData();
            context.CurrentStream.Position = startStreamPosition;

            return (leftArgument: leftArgument, symbol: symbol);
        }
    }
}
