using System;
using System.IO;

namespace GrammarParser {

    class Program {

        static void Main(string[] args) {
            Console.WriteLine("Hello World!");


            var str = "1234567890";

            var mems = new MemoryStream();
            var writer = new StreamWriter(mems);

            writer.Write(str);
            writer.Flush();
            mems.Position = 0;

            var reader = new StreamReader(mems);
            reader.ReadToEnd();
            Console.WriteLine(mems.Position);
            Console.WriteLine(reader.Peek());
            reader.DiscardBufferedData();
            mems.Position = mems.Position - 2;
            Console.WriteLine((char)reader.Peek());

            Console.ReadKey();

        }
    }
}
