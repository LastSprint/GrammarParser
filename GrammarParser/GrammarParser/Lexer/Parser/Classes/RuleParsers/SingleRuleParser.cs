using System;
using System.IO;
using System.Linq;

using GrammarParser.Lexer.Parser.Exceptions;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Types.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers {

    public abstract class SingleRuleParser: IParser {

        protected abstract string TerminateSymbol { get; }

        public bool IsCurrentRule(IParserImmutableContext context)
        {
            var (argument, symbol) = this.ProcessContext(context: context);

            return symbol.Equals(this.TerminateSymbol) && argument != null;
        }

        public abstract IRule Parse(IParserImmutableContext context);

        protected IRule TryParse(IParserImmutableContext context) {

            var (argument, symbol) = this.ProcessContext(context: context);

            if (!symbol.Equals(this.TerminateSymbol)) {
                return null;
            }

            if (argument == null) {
                throw new RuleParserNotExistedLeftArgumentException(context: context,
                    ruleSymbol: $"{this.TerminateSymbol}");
            }

            context.CurrentStream.Position += 1;

            return argument;
        }

        protected (IRule leftArgument, string symbol) ProcessContext(IParserImmutableContext context) {
            var startStreamPosition = context.CurrentStream.Position;
            var reader = new StreamReader(context.CurrentStream);
            var symbol = (char)reader.Read();
            IRule leftArgument;

            try {
                leftArgument = context.CurrentRuleCollection.First();
            } catch (InvalidOperationException) {
                leftArgument = null;
            }

            reader.DiscardBufferedData();
            context.CurrentStream.Position = startStreamPosition;

            return (leftArgument: leftArgument, symbol: symbol.ToString());
        }

    }
}
