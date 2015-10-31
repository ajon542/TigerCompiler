namespace UnitTests
{
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
        public void TestMulNum()
        {
            RunParser(false, 
                TokenType.Mul, 
                TokenType.Num, 
                TokenType.Eof);
        }

        /// <summary>
        /// Run the parser on a list of tokens.
        /// </summary>
        /// <param name="expect">The expected result of the parse.</param>
        /// <param name="tokenTypes">The type of tokens to pass to the parser.</param>
        private void RunParser(bool expect, params TokenType[] tokenTypes)
        {
            // Construct the token list.
            List<Token> tokens = new List<Token>();
            foreach(TokenType type in tokenTypes)
            {
                tokens.Add(new Token(type, 0, 0));
            }

            // Run the parser.
            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            // Check the result.
            Assert.Equals(result, expect);
        }
    }
}
