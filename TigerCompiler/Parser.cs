namespace TigerCompiler
{
    using System;
    using System.Collections.Generic;

    // Grammar
    // (epsilon = e)
    // S -> aABC
    // A -> a | bb
    // B -> a | e
    // C -> b | e

    // | Symbol | First  | Follow    |
    // | -------|--------|-----------|
    // | S      | {a}    | {$}       |
    // | A      | {a, b} | {a, b, $} |
    // | B      | {a, e} | {b, $}    |
    // | C      | {b, e} | {$}       |
    // -------------------------------

    // Parsing Table
    //     | a         | b         | $       |
    // ----|-----------|-----------|---------|
    // S   | S -> aABC |           |         |
    // A   | A -> a    | A -> bb   |         |
    // B   | B -> a    | B -> e    | B -> e  |
    // C   |           | C -> b    | C -> e  |
    // --------------------------------------------------

    // From the parsing table we can see there are no duplicate
    // entries in any of the cells. This means the grammar is LL(1).

    // Parser based on the parsing table generated i.e. without backtracking.
    class PredictiveParser
    {
        int next;
        List<Token> tokens;

        private void S()
        {
            Token token = Next();

            switch(token.Type)
            {
                case TokenType.A:
                    A(); B(); C();
                    break;
                default:
                    Console.WriteLine("Syntax error: expected TokenType.A, got {0}", token.Type);
                    break;
            }
        }

        private void A()
        {
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.A:
                    break;
                case TokenType.B:
                    token = Next();
                    if(token == null)
                    {
                        Console.WriteLine("Syntax error: expected TokenType.B, no more tokens to parse");
                    }
                    else if(token.Type != TokenType.B)
                    {
                        Console.WriteLine("Syntax error: expected TokenType.B, got {0}", token.Type);
                    }

                    break;
                default:
                    Console.WriteLine("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type);
                    break;
            }
        }

        private void B()
        {
            // When the value can go to epsilon, we need a way of not consuming the Token.
            int save = next;

            Token token = Next();

            switch (token.Type)
            {
                case TokenType.A:
                    break;
                case TokenType.B:
                    // Epsilon transition.
                    next = save;
                    break;
                case TokenType.Eof:
                    // Epsilon transition.
                    next = save;
                    break;
                default:
                    Console.WriteLine("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type);
                    break;
            }
        }

        private void C()
        {
            // When the value can go to epsilon, we need a way of not consuming the Token.
            int save = next;

            Token token = Next();

            switch (token.Type)
            {
                case TokenType.B:
                    break;
                case TokenType.Eof:
                    // Epsilon transition.
                    next = save;
                    break;
                default:
                    Console.WriteLine("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type);
                    break;
            }
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            this.tokens = tokens;

            S();

            return true;
        }

        private Token Next()
        {
            if(next < tokens.Count)
            {
                return tokens[next++];
            }

            return null;
        }
    }

    class BacktrackingParser
    {
        int next;
        List<Token> tokens;

        private bool S()
        {
            // S -> aABC
            bool result = Expect(TokenType.A) && A() && B() && C() && Expect(TokenType.Eof);
            return result;
        }

        private bool A()
        {
            int save = next;

            // A -> a
            bool result = (Expect(TokenType.A));
            if (result)
            {
                return true;
            }

            // Backtrack
            next = save;

            // A -> bb
            result = Expect(TokenType.B) && Expect(TokenType.B);
            return result;
        }

        private bool B()
        {
            int save = next;

            // B -> a
            bool result = (Expect(TokenType.A));
            if (result)
            {
                return true;
            }

            // Backtrack
            next = save;

            // B -> e
            return false;
        }

        private bool C()
        {
            int save = next;

            // C -> b
            bool result = (Expect(TokenType.B));
            if (result)
            {
                return true;
            }

            // Backtrack
            next = save;

            // C -> e
            return false;
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            this.tokens = tokens;

            bool result = S();
  
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
