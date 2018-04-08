using System.IO;
using System.Text;

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

        /// <summary>
        /// Достает строчку из потока.
        /// </summary>
        /// <param name="startPosition">Индекс с которого нчинается строка</param>
        /// <param name="endPosition">Индекс на котором стркоа заканчивается</param>
        /// <returns>Bpdktxtyyfz cnhrjf</returns>
        public static string TakeOutString(this Stream stream, long startPosition, long endPosition) {
            var startPos = stream.Position;

            stream.Position = 0;

            var copy = new MemoryStream();

            stream.CopyTo(copy);

            stream.Position = startPos;

            copy.Position = startPosition == 0 ? 0 : startPosition -1;

            var reader = new StreamReader(copy);

            var builder = new StringBuilder();

            for (var i = 0; i < endPosition && !reader.EndOfStream; i++) {
                builder.Append((char)reader.Read());
            }

            reader.DiscardBufferedData();
            reader.Close();
            copy.Close();

            return builder.ToString();
        }

        public static char? NextWithSkipedEmpty(this Stream stream) {
            var startPos = stream.Position;
            var reader = new StreamReader(stream);
            char readed;
            var index = 0;

            do {

                if (reader.EndOfStream) {
                    stream.Position = startPos;
                    return null;
                }

                readed = (char)reader.Read();
                index++;
            } while (CharacterSet.SpaceSymbols.Contains(readed));

            stream.Position = startPos + index - 1;
            return readed;
        }
    }
}
