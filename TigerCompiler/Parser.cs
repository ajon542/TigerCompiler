namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Parser
    {
        int next;
        List<Token> tokens;

        private bool IsToken(TokenType tokenType, Token token)
        {
            return tokenType == token.Type;
        }

        private bool S()
        {
            int save = next;
            return (IsToken(TokenType.LParen, tokens[next++]) && 
                    T() && 
                    IsToken(TokenType.RParen, tokens[next++]));
        }

        private bool T()
        {
            int save = next;
            return IsToken(TokenType.Id, tokens[next++]);
        }

        public Parser()
        {
            tokens = new List<Token>{ new Token(TokenType.LParen), new Token(TokenType.Id, "Int"), new Token(TokenType.RParen) };

            bool accept = S();
        }
    }
}
