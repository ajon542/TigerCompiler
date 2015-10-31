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
    //       | a         | b         | $       |
    // |-----|-----------|-----------|---------|
    // | S   | S -> aABC |           |         |
    // | A   | A -> a    | A -> bb   |         |
    // | B   | B -> a    | B -> e    | B -> e  |
    // | C   |           | C -> b    | C -> e  |
    // -----------------------------------------

    // From the parsing table we can see there are no duplicate
    // entries in any of the cells. This means the grammar is LL(1).

    // Parser based on the parsing table generated i.e. without backtracking.
    class PredictiveParser
    {
        int next;
        List<Token> tokens;
        int errorCount;

        /// <summary>
        /// The starting production.
        /// </summary>
        private void S()
        {
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.A:
                    A(); B(); C();
                    break;
                default:
                    Error(string.Format("Syntax error: expected TokenType.A, got {0}", token.Type));
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
                    if (token == null)
                    {
                        Error(string.Format("Syntax error: expected TokenType.B, no more tokens to parse"));
                    }
                    else if (token.Type != TokenType.B)
                    {
                        Error(string.Format("Syntax error: expected TokenType.B, got {0}", token.Type));
                    }

                    break;
                default:
                    Error(string.Format("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type));
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
                    Error(string.Format("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type));
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
                    Error(string.Format("Syntax error: expected TokenType.A or TokenType.B, got {0}", token.Type));
                    break;
            }
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            errorCount = 0;
            this.tokens = tokens;

            S();

            return (errorCount == 0) && (Next().Type == TokenType.Eof);
        }

        private void Error(string errorString)
        {
            errorCount++;
            Console.WriteLine(errorString);
        }

        private Token Next()
        {
            if (next < tokens.Count)
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
            return true;
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
            return true;
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

    // Grammar
    // (epsilon = e)
    // S  -> E
    // E  -> TE'
    // E' -> +TE'
    // E' -> -TE'
    // E' -> e
    // T  -> FT'
    // T' -> *FT'
    // T' -> /FT'
    // T' -> e
    // F  -> id
    // F  -> num
    // F  -> (E)

    // | Symbol | First        | Follow             |
    // | -------|--------------|--------------------|
    // | S      | {id, num, (} | {$}                |
    // | E      | {id, num, (} | {$, )}             |
    // | E'     | {+, -, e}    | {$, )}             |
    // | T      | {id, num, (} | {$, +, -, )}       |
    // | T'     | {*, /, e}    | {$, +, -, )}       |
    // | F      | {id, num, (} | {$, +, -, *, /, )} |
    // ----------------------------------------------

    // Parsing Table
    //       | +          | -          | *          | /          | id       | num      | (        | )       | $       |
    // |-----|------------|------------|------------|------------|----------|----------|----------|---------|---------|
    // | S   | na         | na         | na         | na         | S -> E   | S -> E   | S -> E   | na      | na      |
    // | E   | na         | na         | na         | na         | E -> TE' | E -> TE' | E -> TE' | na      | na      |
    // | E'  | E' -> +TE' | E' -> -TE' | na         | na         | na       | na       | na       | E' -> e | E' -> e |
    // | T   | na         | na         | na         | na         | T -> FT' | T -> FT' | T -> FT' | na      | na      |
    // | T'  | T' -> e    | T' -> e    | T' -> *FT' | T' -> /FT' | na       | na       | na       | T' -> e | T' -> e |
    // | F   | na         | na         | na         | na         | F -> id  | F -> num | F -> (E) | na      | na      |
    // ----------------------------------------------------------------------------------------------------------------
    class Parser
    {
        int next;
        List<Token> tokens;
        int errorCount;

        private void S()
        {
            int save = next;
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.Id:
                case TokenType.Num:
                case TokenType.LParen:
                    // Do not consume the character yet.
                    next = save;
                    E();
                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ id, num, ( ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        private void E()
        {
            int save = next;
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.Id:
                case TokenType.Num:
                case TokenType.LParen:
                    // Do not consume the character yet.
                    next = save;
                    T();
                    Ep();
                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ id, num, ( ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        private void Ep()
        {
            int save = next;

            Token token = Next();

            switch (token.Type)
            {
                // E' -> +TE'
                // E' -> -TE'
                case TokenType.Plus:
                case TokenType.Minus:
                    T();
                    Ep();
                    break;

                // Epsilon transitions.
                case TokenType.RParen:
                case TokenType.Eof:
                    next = save;
                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ +, -, ), Eof ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        private void T()
        {
            int save = next;
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.Id:
                case TokenType.Num:
                case TokenType.LParen:
                    // Do not consume the character yet.
                    next = save;
                    F();
                    Tp();
                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ id, num, ( ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        private void Tp()
        {
            int save = next;

            Token token = Next();

            switch (token.Type)
            {
                // T' -> *FT'
                // T' -> /FT'
                case TokenType.Mul:
                case TokenType.Div:
                    F();
                    Tp();
                    break;

                // Epsilon transitions.
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.RParen:
                case TokenType.Eof:
                    next = save;
                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ +, -, *, /, ), Eof ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        private void F()
        {
            Token token = Next();

            switch (token.Type)
            {
                case TokenType.Id:
                case TokenType.Num:
                    break;

                // F -> (E)
                case TokenType.LParen:
                    E();
                    
                    token = Next();
                    if (token == null)
                    {
                        Error(string.Format("Syntax error: no more tokens to parse"));
                    }
                    else if (token.Type != TokenType.RParen)
                    {
                        Error(string.Format("Syntax error({0},{1}): expected [ ) ], got {2}", token.Line, token.Column, token.Type));
                    }

                    break;
                default:
                    Error(string.Format("Syntax error({0},{1}): expected [ id, num, ( ], got {2}", token.Line, token.Column, token.Type));
                    break;
            }
        }

        public bool Parse(List<Token> tokens)
        {
            next = 0;
            errorCount = 0;
            this.tokens = tokens;

            S();

            return (errorCount == 0) && (Next().Type == TokenType.Eof);
        }

        private void Error(string errorString)
        {
            errorCount++;
            Console.WriteLine(errorString);
        }

        private Token Next()
        {
            if (next < tokens.Count)
            {
                return tokens[next++];
            }

            return new Token(TokenType.Eof, 0, 0);
        }
    }
}
