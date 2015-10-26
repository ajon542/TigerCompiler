namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Parser
    {
        // Example Grammar
        // E -> T + E
        // E -> T
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
                Console.WriteLine("E1");
                return true;
            }
            next = save;

            if (E2())
            {
                Console.WriteLine("E2");
                return true;
            }

            return false;
        }

        private bool E1()
        {
            bool result = T() && IsToken(TokenType.Plus) && E();
            return result;
        }

        private bool E2()
        {
            bool result = T();
            return result;
        }

        private bool T()
        {
            int save = next;

            if(T1())
            {
                Console.WriteLine("T1");
                return true;
            }
            next = save;

            if(T2())
            {
                Console.WriteLine("T2");
                return true;
            }
            next = save;

            if(T3())
            {
                Console.WriteLine("T3");
                return true;
            }

            return false;
        }

        private bool T1()
        {
            bool result = IsToken(TokenType.Id);
            return result;
        }

        private bool T2()
        {
            bool result = IsToken(TokenType.Id) && IsToken(TokenType.Multiply) && T();
            return result;
        }

        private bool T3()
        {
            bool result = IsToken(TokenType.LParen) && E() && IsToken(TokenType.RParen);
            return result;
        }

        private bool IsToken(TokenType tokenType)
        {
            if (next < tokens.Count)
            {
                bool result = tokenType == tokens[next++].Type;
                return result;
            }
            else
            {
                return false;
            }
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            bool result = true;
            this.tokens = tokens;

            // Parse the entire input.
            // TODO: Error checking.
            while(next != tokens.Count)
            {
                result &= E();
            }

            return result;
        }
    }
}
