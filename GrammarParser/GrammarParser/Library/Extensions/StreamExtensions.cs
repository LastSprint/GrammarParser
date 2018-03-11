using System.IO;

namespace GrammarParser.Library.Extensions {

    public static class StreamExtensions {

        /// <summary>
        /// Возвращает символ, на котором в данный момент остановился поток.
        /// Операция является Immutable (то есть положение указателя чтения не меняется)
        /// В случае, поток окончился, то вовзращается null
        /// </summary>
        /// <returns>Символ или null</returns>
        public static char? CurrentSymbol(this Stream stream) {
            var streamPos = stream.Position;
            var reader = new StreamReader(stream);
            var isEndOfStream = reader.EndOfStream;
            var currentChar = isEndOfStream ? new char?() : (char)reader.Peek();
            reader.DiscardBufferedData();
            stream.Position = streamPos;
            return currentChar;
        }

    }
}
