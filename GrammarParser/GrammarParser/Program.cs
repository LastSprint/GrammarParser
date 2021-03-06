﻿using System;
using System.IO;

using GrammarParser.Lexer.Injections.Injectors;
using GrammarParser.Lexer.RuleLexer.Configurations;

namespace GrammarParser {

    class Program {

        public static class MemoryStreamExtension {

            public static MemoryStream FromString(MemoryStream stream, string value) {
                var writer = new StreamWriter(stream);
                writer.Write(value);
                writer.Flush();
                stream.Position = 0;
                return stream;
            }
        }

        static void Main(string[] args) {
            Console.WriteLine("Введи патерн");

            var str = Console.ReadLine();

            var mems = MemoryStreamExtension.FromString(new MemoryStream(), str);

            var lex = new Lexer.RuleLexer.Lexer(new SimpleParserInjector().Injection(), new LexerBuilder(new SimpleParserInjector()));
            var tree = new AstTree.AstTree(lex.Parse(mems));

            Console.WriteLine("Вводи свои слова");

            var inp = Console.ReadLine();

            do {
                var stre = MemoryStreamExtension.FromString(new MemoryStream(), inp);
                Console.WriteLine($"Результат проверки для \'{str}\': {tree.Check(stre)}");
                Console.WriteLine("ЩА ПОКАЖУ ДЕРЕВО!");
                Console.WriteLine();
                Console.WriteLine();
                tree.DeepWalk(x=>Console.WriteLine($"{x.ParsedResult}"));
                inp = Console.ReadLine();
            } while (inp != "DIE");

            Console.WriteLine("Давай, дасведания");

            Console.ReadKey();

        }
    }
}
