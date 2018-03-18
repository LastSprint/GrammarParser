using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GrammarParser.Lexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Parser.Interfaces;
using GrammarParser.Lexer.RuleLexer.Rules.Interfaces;
using GrammarParser.Lexer.StructureLexer.Models;
using GrammarParser.Lexer.StructureLexer.Parsers.Exceptions;
using GrammarParser.Lexer.StructureLexer.Rules;

namespace GrammarParser.Lexer.StructureLexer.Parsers {

    /// <summary>
    /// Низкоуровневый парсер.
    /// Не использует сам GrammarParser для работы.
    /// Считает, что должен получить на вход следующую структуру данных:
    /// AnySymbols: AnySymbols =>AnySymbols: "AnySymbols" ....   
    /// </summary>
    public class UserRuleParser: IParser {

        public const char RuleEndTerminator = ';';
        public const char RuleNameEndTerminator = ':';
        public const char TokenExpressionDivider = ',';
        public const char TokenKeyValueDivider = ':';

        public const string ConvertionOperator = "=>";
        public const string NameTokenString = "Name";

        

        public bool IsCurrentRule(IParserImmutableContext context) {
            var (name, rulePattern, tokenPattern) = this.TryParse(context);
            var condition = string.IsNullOrEmpty(name) && string.IsNullOrEmpty(rulePattern) &&
                            string.IsNullOrEmpty(tokenPattern);

            return !condition;
        }

        public IRule Parse(IParserImmutableContext context) {

            var (name, rulePattern, tokenPattern) = this.TryParse(context);

            if (string.IsNullOrEmpty(name)) {
                throw new UserRuleParserBadNameException();
            }

            if (string.IsNullOrEmpty(rulePattern)) {
                throw new UserRuleParserBadRulePatternException();
            }

            if (string.IsNullOrEmpty(tokenPattern)) {
                throw new UserRuleParserBadTokenExpressionException();
            }

            var tokenExpression = this.ParseTokenExpression(tokenPattern);

            if (tokenExpression == null) {
                throw new UserRuleParserBadTokenExpressionException();
            }

            var rule = new UserRule(name: name, rulePattern: rulePattern, tokenConvertionPattern: tokenExpression);

            if (!rule.IsValid()) {
                throw new UserRuleParserBadTokenExpressionException();
            }

            return rule;
        }

        private (string name, string rulePattern, string tokenPattern) TryParse(IParserImmutableContext context) {
            var name = this.ExecutePart(context.CurrentStream, RuleNameEndTerminator)
                ?.TrimEnd()
                ?.TrimStart();
            var rulePattern = this.ExecutePart(context.CurrentStream, ConvertionOperator.First(),
                ConvertionOperator.Last())
                ?.Trim();
            var tokenPattern = this.ExecutePart(context.CurrentStream, RuleEndTerminator)
                ?.Trim()
                ?.Remove(0, 1);

            return (name: name, rulePattern: rulePattern, tokenPattern: tokenPattern);
        }

        private string ExecutePart(Stream stream, char partEndTerminator, char? afterPartSymbol = null) {

            var startPosition = stream.Position;
            var reader = new StreamReader(stream);
            var builder = new StringBuilder();

            var symbol = (char)reader.Read();

            var isReaded = false;

            while (!reader.EndOfStream && (symbol != partEndTerminator && (afterPartSymbol == null || reader.Peek() != afterPartSymbol))) {

                builder.Append(symbol);
                symbol = (char)reader.Read();
                isReaded = true;
            }

            reader.DiscardBufferedData();
            stream.Position = startPosition + builder.Length + 1;

            return isReaded ? builder.ToString() : null;
        }

        private TokenExpression ParseTokenExpression(string tokenExpr) {
            var splited = tokenExpr.Split(TokenExpressionDivider);

            if (splited.Length == 0) {
                return null;
            }

            var name = "";

            var childs = new Dictionary<string, int>();

            foreach (var element in splited) {
                var keyValue = element.Split(TokenKeyValueDivider);

                if (keyValue.Length != 2) {
                    return null;
                }

                if (keyValue[0] == NameTokenString) {
                    name = keyValue[1]
                        .Replace("\"", string.Empty)
                        .TrimEnd()
                        .TrimStart();
                    continue;
                }

                if (!int.TryParse(keyValue[1], out var intResult)) {
                    return null;
                }

                childs.Add(keyValue[0], intResult);
            }

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) {
                return null;
            }

            return new TokenExpression(name, childs);
        }

    }
}
