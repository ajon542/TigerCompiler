
namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static void ErrorHandler(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Unknown token at line: {0}, position: {1}", e.Line, e.LineOffset);
        }

        static void Main(string[] args)
        {
            Lexer lexer = new Lexer("if then else blah *\n * (123) {abc   } ab1234 123ab\n andrew jones");
            lexer.ErrorEventHandler += ErrorHandler;

            List<Token> tokens = lexer.Tokenize();

            Parser parser = new Parser(tokens);

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey(true);
        }
    }
}
