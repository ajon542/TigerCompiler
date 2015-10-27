namespace UnitTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TigerCompiler;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void TestEmptyTokenList()
        {
            List<Token> tokens = new List<Token> { new Token(TokenType.Eof) };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestId()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Id), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestPlus()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Plus), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIdPlus()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Id), 
                new Token(TokenType.Plus),
                new Token(TokenType.Eof) };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIdPlusId()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Id), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIdPlusIdPlus()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Id), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlusIdPlusId()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestPlusPlusId()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Plus), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestIdPlusPlusId()
        {
            List<Token> tokens = new List<Token> 
            { 
                new Token(TokenType.Id),
                new Token(TokenType.Plus), 
                new Token(TokenType.Plus), 
                new Token(TokenType.Id), 
                new Token(TokenType.Eof) 
            };

            Parser parser = new Parser();
            bool result = parser.Parse(tokens);

            Assert.IsFalse(result);
        }
    }
}
