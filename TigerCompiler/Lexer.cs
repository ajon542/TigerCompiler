
namespace TigerCompiler
{
    using System;
    using System.Text;

    enum TokenType
    {
        Unknown,
        Id,
        Num, 
        Eof,
        LBrace,
        RBrace,
        LParen,
        RParen,
        Comma
    }

    class Token
    {
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value = "")
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", Type);
            if(Value != string.Empty)
            {
                sb.AppendFormat("({0})", Value);
            }

            return sb.ToString();
        }
    }

    class Scanner
    {
        private int inputIndex;
        private readonly string input;
        public int Line { get; private set; }
        public int LineOffset { get; private set; }
        public char Ch { get; private set; }

        public Scanner(string input)
        {
            this.input = input;
            Ch = ' '; // Default to whitespace, it will be consumed on a scan.
        }

        public void Next()
        {
            if (inputIndex < input.Length)
            {
                Ch = input[inputIndex++];
                LineOffset++;
            }
            else
            {
                Ch = '\0';
            }

            if(Ch == '\n')
            {
                LineOffset = 0;
                Line++;
            }
        }

        public void ConsumeWhiteSpace()
        {
            while (Char.IsWhiteSpace(Ch))
            {
                Next();
            }
        }

        public string ReadIdentifier()
        {
            StringBuilder sb = new StringBuilder();

            while (Char.IsLetterOrDigit(Ch))
            {
                sb.Append(Ch);
                Next();
            }

            return sb.ToString();
        }

        public string ReadNumber()
        {
            StringBuilder sb = new StringBuilder();

            while (Char.IsDigit(Ch))
            {
                sb.Append(Ch);
                Next();
            }

            return sb.ToString();
        }
    }

    class Lexer
    {
        private Scanner scanner;

        public Lexer(string input)
        {
            scanner = new Scanner(input);
        }

        private void Scan(out Token token)
        {
            token = new Token(TokenType.Unknown);

            scanner.ConsumeWhiteSpace();

            if (Char.IsLetter(scanner.Ch))
            {
                token = new Token(TokenType.Id, scanner.ReadIdentifier());
            }
            else if (Char.IsDigit(scanner.Ch))
            {
                token = new Token(TokenType.Num, scanner.ReadNumber());
            }
            else
            {
                if (scanner.Ch == '\0')
                {
                    token = new Token(TokenType.Eof);
                }
                else if (scanner.Ch == '{')
                {
                    token = new Token(TokenType.LBrace);
                }
                else if (scanner.Ch == '}')
                {
                    token = new Token(TokenType.RBrace);
                }
                else if (scanner.Ch == '(')
                {
                    token = new Token(TokenType.LParen);
                }
                else if (scanner.Ch == ')')
                {
                    token = new Token(TokenType.RParen);
                }
                else if (scanner.Ch == ',')
                {
                    token = new Token(TokenType.Comma);
                }
                else
                {
                    Console.WriteLine("Unknown token {0} at line: {1}, position: {2}", scanner.Ch, scanner.Line, scanner.LineOffset);
                }

                scanner.Next();
            }

            Console.WriteLine(token);
        }

        public void Tokenize()
        {
            Token token;
            Scan(out token);

            while (token.Type != TokenType.Eof)
            {
                Scan(out token);
            }
        }
    }
}
