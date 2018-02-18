using System.IO;

namespace GrammarParserUnitTests.Utils {

    public static class MemoryStreamExtension {

        public static MemoryStream FromString(this MemoryStream stream, string value) {
            var writer = new StreamWriter(stream);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static MemoryStream FromChar(this MemoryStream stream, char value) => FromString(stream: stream, value: value.ToString());
    }
}
