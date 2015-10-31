namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TigerCompiler;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestId()
        {
            RunParser(true,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestNum()
        {
            RunParser(true,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestPlusId()
        {
            RunParser(false,
                TokenType.Plus,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestPlusNum()
        {
            RunParser(false,
                TokenType.Plus,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestMinusId()
        {
            RunParser(false,
                TokenType.Minus,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestMinusNum()
        {
            RunParser(false,
                TokenType.Minus,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestMulId()
        {
            RunParser(false,
                TokenType.Mul,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestMulNum()
        {
            RunParser(false,
                TokenType.Mul,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestDivId()
        {
            RunParser(false,
                TokenType.Div,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestDivNum()
        {
            RunParser(false,
                TokenType.Div,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestIdPlusId()
        {
            RunParser(true,
                TokenType.Id,
                TokenType.Plus,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestNumPlusNum()
        {
            RunParser(true,
                TokenType.Num,
                TokenType.Plus,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestIdMinusId()
        {
            RunParser(true,
                TokenType.Id,
                TokenType.Minus,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestNumMinusNum()
        {
            RunParser(true,
                TokenType.Num,
                TokenType.Minus,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestIdMulId()
        {
            RunParser(true,
                TokenType.Id,
                TokenType.Mul,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestNumMulNum()
        {
            RunParser(true,
                TokenType.Num,
                TokenType.Mul,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestIdDivId()
        {
            RunParser(true,
                TokenType.Id,
                TokenType.Div,
                TokenType.Id,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestNumDivNum()
        {
            RunParser(true,
                TokenType.Num,
                TokenType.Div,
                TokenType.Num,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestParenIdParen()
        {
            RunParser(true,
                TokenType.LParen,
                TokenType.Id,
                TokenType.RParen,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestParenIdPlusIdParen()
        {
            RunParser(true,
                TokenType.LParen,
                TokenType.Id,
                TokenType.Plus,
                TokenType.Id,
                TokenType.RParen,
                TokenType.Eof);
        }

        [TestMethod]
        public void TestHangingRightParen()
        {
            RunParser(false,
                TokenType.Id,
                TokenType.Plus,
                TokenType.Id,
                TokenType.RParen,
                TokenType.Eof);
            throw new Exception("Interesting test, Parsing does fail, but we don't get any syntax error print.");
        }

        /// <summary>
        /// Run the parser on a list of tokens.
        /// </summary>
        /// <param name="expect">The expected result of the parse.</param>
        /// <param name="tokenTypes">The type of tokens to pass to the parser.</param>
        private void RunParser(bool expect, params TokenType[] tokenTypes)
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

            // Check the result.
            Assert.AreEqual(result, expect);
        }
    }
}
