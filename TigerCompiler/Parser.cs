namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    class Parser
    {
        // Grammar
        // E -> T + E
        // E -> T
        // T -> id

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

        private bool T()
        {
            if (T1())
            {
                return true;
            }

            return false;
        }

        private bool E1()
        {
            // E -> T + E
            bool result = T() && Expect(TokenType.Plus) && E();
            Console.WriteLine("E -> T + E : " + result + ", next=" + next);
            return result;
        }

        private bool E2()
        {
            // E -> T
            bool result = T();
            Console.WriteLine("E -> T : " + result + ", next=" + next);
            return result;
        }

        private bool T1()
        {
            // T -> id
            bool result = Expect(TokenType.Id);
            Console.WriteLine("T -> id : " + result + ", next=" + next);
            return result;
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            bool result = true;
            this.tokens = tokens;

            result &= E();
            result &= Expect(TokenType.Eof);
  
            return result;
        }

        private bool Expect(TokenType tokenType)
        {
            if (next < tokens.Count)
            {
                bool result = tokenType == tokens[next++].Type;
                return result;
            }
            return false;
        }
    }

    class ParserWithSomeBug
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
                //Console.WriteLine("E1");
                return true;
            }
            next = save;

            if (E2())
            {
                //Console.WriteLine("E2");
                return true;
            }

            return false;
        }

        private bool T()
        {
            int save = next;

            if (T1())
            {
                //Console.WriteLine("T1");
                return true;
            }
            next = save;

            if (T2())
            {
                //Console.WriteLine("T2");
                return true;
            }
            next = save;

            if (T3())
            {
                //Console.WriteLine("T3");
                return true;
            }

            return false;
        }

        private bool E1()
        {
            // E -> T + E
            bool result = T() && Expect(TokenType.Plus) && E();
            Console.WriteLine("E -> T + E : " + result + ", next=" + next);
            return result;
        }

        private bool E2()
        {
            // E -> T
            bool result = T();
            Console.WriteLine("E -> T : " + result + ", next=" + next);
            return result;
        }

        private bool T1()
        {
            // T -> id
            bool result = Expect(TokenType.Id);
            Console.WriteLine("T -> id : " + result + ", next=" + next);
            return result;
        }

        private bool T2()
        {
            // T -> id * T
            bool result = Expect(TokenType.Id) && Expect(TokenType.Multiply) && T();
            Console.WriteLine("T -> id * T : " + result + ", next=" + next);
            return result;
        }

        private bool T3()
        {
            // T -> (E)
            bool result = Expect(TokenType.LParen) && E() && Expect(TokenType.RParen);
            Console.WriteLine("T -> (E) : " + result + ", next=" + next);
            return result;
        }

        private bool Expect(TokenType tokenType)
        {
            if (next < tokens.Count)
            {
                bool result = tokenType == tokens[next++].Type;
                return result;
            }
            return false;
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
