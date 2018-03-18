using System;

using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public abstract class TwoArgumentRuleParser : SingleRuleParser {

        public override bool IsCurrentRule(IParserImmutableContext context) {
            var streamStartPos = context.CurrentStream.Position;

            if (!base.IsCurrentRule(context)) {
                return false;
            }

            context.CurrentStream.Position += this.TerminateSymbol.Length;

            var result = false;

            try {
                result = this.ParseRightArgument(context) != null;
            }
            catch (ArgumentOutOfRangeException) {
                return false;
            }
            finally {
                context.CurrentStream.Position = streamStartPos;
            }

            return result;
        }

        public abstract override IRule Parse(IParserImmutableContext conext);

        protected IRule ParseRightArgument(IParserImmutableContext context) => context.LexerBuilder
            .Build(context.CurrentStream)
            .ParseNextRule(context.CurrentStream);

    }

}