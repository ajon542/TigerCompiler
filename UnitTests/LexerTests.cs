
namespace UnitTests
{
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

            CheckTokens(tokens,
                TokenType.Eof);
        }

        /// <summary>
        /// The lexer should generate all the tokens.
        /// </summary>
        [TestMethod]
        public void TestAllTokens()
        {
            Lexer lexer = new Lexer("abc 123 { } ( ) , ; if else");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Id,
                TokenType.Num,
                TokenType.LBrace,
                TokenType.RBrace,
                TokenType.LParen,
                TokenType.RParen,
                TokenType.Comma,
                TokenType.Semicolon,
                TokenType.If,
                TokenType.Else,
                TokenType.Eof);
        }

        /// <summary>
        /// The lexer should generate the correct identifier token.
        /// </summary>
        [TestMethod]
        public void TestIdToken1()
        {
            Lexer lexer = new Lexer("abc");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Id,
                TokenType.Eof);

            Assert.AreEqual(tokens[0].Value, "abc");
        }

        /// <summary>
        /// The lexer should generate the correct identifier token.
        /// </summary>
        [TestMethod]
        public void TestIdToken2()
        {
            Lexer lexer = new Lexer("abc123");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Id,
                TokenType.Eof);

            Assert.AreEqual(tokens[0].Value, "abc123");
        }

        /// <summary>
        /// The lexer should generate the correct number token.
        /// </summary>
        [TestMethod]
        public void TestNumToken1()
        {
            Lexer lexer = new Lexer("123");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Num,
                TokenType.Eof);

            Assert.AreEqual(tokens[0].Value, "123");
        }

        /// <summary>
        /// The lexer should generate the correct number token.
        /// </summary>
        [TestMethod]
        public void TestNumToken2()
        {
            Lexer lexer = new Lexer("123abc");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Num,
                TokenType.Id,
                TokenType.Eof);

            Assert.AreEqual(tokens[0].Value, "123");
            Assert.AreEqual(tokens[1].Value, "abc");
        }

        /// <summary>
        /// The lexer should generate the correct number token.
        /// </summary>
        [TestMethod]
        public void TestIdAndParentheses()
        {
            Lexer lexer = new Lexer("print()");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens,
                TokenType.Id,
                TokenType.LParen,
                TokenType.RParen,
                TokenType.Eof);

            Assert.AreEqual(tokens[0].Value, "print");
        }

        /// <summary>
        /// The lexer should generate the correct tokens for "if(){}".
        /// </summary>
        [TestMethod]
        public void TestIfAndParentheses()
        {
            Lexer lexer = new Lexer("\n    \nif  \n\n(\n \n)  \n\n{    \n }");
            List<Token> tokens = lexer.Tokenize();

            CheckTokens(tokens, 
                TokenType.If, 
                TokenType.LParen, 
                TokenType.RParen, 
                TokenType.LBrace, 
                TokenType.RBrace, 
                TokenType.Eof);
        }

        private void CheckTokens(List<Token> tokens, params TokenType[] expectedTokenTypes)
        {
            if (tokens.Count != expectedTokenTypes.Length)
            {
                Assert.Fail("The lexer should generate all the tokens.");
            }

            for (int i = 0; i < tokens.Count; ++i)
            {
                Assert.AreEqual(tokens[i].Type, expectedTokenTypes[i]);
            }
        }
    }
}
