using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GrammarParser.Lexer.Injections;
using GrammarParser.Lexer.Parser.Classes;
using GrammarParser.Lexer.Parser.Interfaces;

namespace GrammarParser.Lexer.Configurations {
    
    public class Lexer: ILexer {

        /// <summary>
        /// Состояние лексера
        /// </summary>
        private enum State {
            NoGroup,
            Group
        }

        private IParser _nonArgumentRuleParser;
        private IParser _oneArgumentRuleParser;
        private IParser _twoArgumentRuleParser;

        private DefaultParserContext _context;

        private State _state;

        public Lexer(IParserProvider provider) {
            // TODO: Подумать, быть может лучше будет использовтаь какой-нить провайдер.
            this._nonArgumentRuleParser = provider.NonArgumentParser;
            this._oneArgumentRuleParser = provider.OneArgumentParer;
            this._twoArgumentRuleParser = provider.TwoArgumentParser;

            this._state = State.NoGroup;
        }

        public IParserContext Parse(Stream stream) {
            this._context = new DefaultParserContext(stream: stream);
            var symdol = this.GetCurrentSymbol();

           
        }

        private char GetCurrentSymbol() {

            var streamPos = this._context.CurrentStream.Position;
            var reader = new StreamReader(this._context.CurrentStream);
            var currentChar = (char)reader.Peek();
            reader.DiscardBufferedData();
            this._context.CurrentStream.Position = streamPos;

            return currentChar;
        }
    }
}
