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

        /// <summary>
        /// Возвращает символ, следующий за тем на который указывает указатель чтения стрима.`  
        /// Операция является Immutable (то есть положение указателя чтения не меняется)
        /// В случае, поток окончился, то вовзращается null
        /// </summary>
        /// <returns>Символ или null</returns>
        public static char? Peek(this Stream stream) {
            stream.TryToSeekToNext();
            return stream.CurrentSymbol();
        }

        /// <summary>
        /// Пытается сдвинть указатель потока на седующий символ.
        /// </summary>
        public static void TryToSeekToNext(this Stream stream) {
            var streamPos = stream.Position;
            var reader = new StreamReader(stream);
            var isEndOfStream = reader.EndOfStream;
            if (!isEndOfStream) {
                reader.Read();
            }
            reader.DiscardBufferedData();
            stream.Position = streamPos + 1;
        }

        public static MemoryStream FromString(this MemoryStream stream, string value) {
            var writer = new StreamWriter(stream);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

    }
}
