namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Parser
    {
        // Example Grammar
        // E -> T
        // E -> T + E
        // T -> id
        // T -> id * T
        // T -> (E)

        int next;
        List<Token> tokens;

        private bool E()
        {
            int save = next;

            if (E1())
            {
                return true;
            }
            next = save;

            if (E2())
            {
                return true;
            }

            return false;
        }

        private bool E1()
        {
            return T();
        }

        private bool E2()
        {
            return T() &&
                   IsToken(TokenType.Plus) &&
                   E();
        }

        private bool T()
        {
            int save = next;

            if(T1())
            {
                return true;
            }
            next = save;

            if(T2())
            {
                return true;
            }
            next = save;

            if(T3())
            {
                return true;
            }

            return false;
        }

        private bool T1()
        {
            return IsToken(TokenType.Id);
        }

        private bool T2()
        {
            return IsToken(TokenType.Id) && IsToken(TokenType.Multiply) && T();
        }

        private bool T3()
        {
            return IsToken(TokenType.LParen) && E() && IsToken(TokenType.RParen);
        }

        private bool IsToken(TokenType tokenType)
        {
            if (next < tokens.Count)
            {
                return tokenType == tokens[next++].Type;
            }
            else
            {
                return false;
            }
        }


        public Parser()
        {
            tokens = new List<Token> { new Token(TokenType.LParen), new Token(TokenType.Id), new Token(TokenType.RParen) };

            bool accept = E();
        }
    }
}
