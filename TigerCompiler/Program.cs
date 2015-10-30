﻿
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

            BacktrackingParser btParser = new BacktrackingParser();
            PredictiveParser ptParser = new PredictiveParser();
            //tokens = new List<Token> { new Token(TokenType.Id), new Token(TokenType.Plus), new Token(TokenType.Id), new Token(TokenType.Multiply), new Token(TokenType.Id) };
            //tokens = new List<Token> { new Token(TokenType.LParen), new Token(TokenType.Id), new Token(TokenType.RParen) };
            //Console.WriteLine(parser.Parse(tokens));

            //tokens = new List<Token> { new Token(TokenType.A), new Token(TokenType.A), new Token(TokenType.A), new Token(TokenType.A), new Token(TokenType.B), new Token(TokenType.Eof) };
            tokens = new List<Token> { new Token(TokenType.A), new Token(TokenType.B), new Token(TokenType.B), new Token(TokenType.Eof) };
            Console.WriteLine("Backtracking Parser\n" + btParser.Parse(tokens));
            Console.WriteLine("Predictive Parser\n" + ptParser.Parse(tokens));

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey(true);
        }
    }
}
