
namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static void ErrorHandler(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Unknown token at line: {0}, position: {1}", e.Line, e.Column);
        }

        static void Main(string[] args)
        {
            //Lexer lexer = new Lexer("if then else blah *\n * (123) {abc   } ab1234 123ab\n andrew jones");
            //lexer.ErrorEventHandler += ErrorHandler;

            //List<Token> tokens = lexer.Tokenize();

            // Interesting test, Parsing does fail, but we don't get any syntax error print.
            bool result = RunParser(
                TokenType.Id,
                TokenType.Plus,
                //TokenType.Id,
                TokenType.RParen,
                TokenType.Eof);

            string status = result ? "Parsing succeeded" : "Parsing failed";

            Console.WriteLine(status);

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Run the parser on a list of tokens.
        /// </summary>
        /// <param name="tokenTypes">The type of tokens to pass to the parser.</param>
        private static bool RunParser(params TokenType[] tokenTypes)
        {
            // Fake the column number of the token so the parser
            // can at least specify which token caused the error.
            int column = 0;

            // Construct the token list.
            List<Token> tokens = new List<Token>();
            foreach (TokenType type in tokenTypes)
            {
                tokens.Add(new Token(type, 0, column++));
            }

            // Run the parser.
            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            return result;
        }
    }
}
