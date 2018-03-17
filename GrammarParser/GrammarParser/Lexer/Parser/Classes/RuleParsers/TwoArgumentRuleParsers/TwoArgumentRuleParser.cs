using System.IO;
using System.Linq;
using GrammarParser.Lexer.Parser.Classes.RuleParsers.SingleArgumentRuleParsers;
using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.Rules.Interfaces;
using GrammarParser.Library;

namespace GrammarParser.Lexer.Parser.Classes.RuleParsers.TwoArgumentRuleParsers {

    public abstract class TwoArgumentRuleParser: SingleRuleParser {

        protected readonly IInjector<ILexer> lexerInjector;

        protected TwoArgumentRuleParser(IInjector<ILexer> lexerInjector) => this.lexerInjector = lexerInjector;

        public override bool IsCurrentRule(IParserImmutableContext context) {

            // TODO: Write logick after implement parsing

            if (!base.IsCurrentRule(context))
            {
                return false;
            }

            return false;
        }

        public abstract override IRule Parse(IParserImmutableContext conext);

        protected IRule ParseRightArgument(IParserImmutableContext context) {

            /// Тут нужен лексер, который умеет только одно правило парсить

            var newContext = this.lexerInjector.Injection().Parse(context.CurrentStream).ParsedRules.FirstOrDefault();

            var startStreamPosition = context.CurrentStream.Position;
            var reader = new StreamReader(context.CurrentStream);
            var terminateSequence = "";

            for (var i = 0; i < this.TerminateSymbol.Length; i++) {
                terminateSequence += (char)reader.Read();
            }

            reader.DiscardBufferedData();
            context.CurrentStream.Position = startStreamPosition;

            var leftArgument = context.Peek();



            return leftArgument;
        }

    }
}
  