
namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TigerCompiler;

    [TestClass]
    public class LexerTests
    {
        /// <summary>
        /// The lexer should generate a single end-of-file token for an empty string.
        /// </summary>
        [TestMethod]
        public void TestEmptyString()
        {
            Lexer lexer = new Lexer("");
            List<Token> tokens = lexer.Tokenize();

            if (tokens.Count != 1 || tokens[0].Type != TokenType.Eof)
            {
                Assert.Fail("The lexer should generate a single end-of-file token for an empty string.");
            }
        }

        /// <summary>
        /// The lexer should generate all the tokens.
        /// </summary>
        [TestMethod]
        public void TestAllTokens()
        {
            Lexer lexer = new Lexer("abc 123 { } ( ) , ; if else");
            List<Token> tokens = lexer.Tokenize();

            if (tokens.Count != 11)
            {
                Assert.Fail("The lexer should generate all the tokens.");
            }

            if (tokens[0].Type != TokenType.Id ||
                tokens[1].Type != TokenType.Num ||
                tokens[2].Type != TokenType.LBrace ||
                tokens[3].Type != TokenType.RBrace ||
                tokens[4].Type != TokenType.LParen ||
                tokens[5].Type != TokenType.RParen ||
                tokens[6].Type != TokenType.Comma ||
                tokens[7].Type != TokenType.Semicolon ||
                tokens[8].Type != TokenType.If ||
                tokens[9].Type != TokenType.Else ||
                tokens[10].Type != TokenType.Eof)
            {
                Assert.Fail("The lexer should generate the correct token.");
            }
        }
    }
}
