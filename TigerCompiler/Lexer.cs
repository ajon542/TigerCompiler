
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
        private readonly string input;
        public int Position { get; private set; }
        public char Char { get; private set; }

        public Scanner(string input)
        {
            this.input = input;
            Char = ' '; // Default to whitespace, it will be consumed on a scan.
        }

        public void Next()
        {
            if (Position < input.Length)
            {
                Char = input[Position++];
            }
            else
            {
                Char = '\0';
            }
        }
    }

    class Lexer
    {
        private Scanner scanner;

        public Lexer(string input)
        {
            scanner = new Scanner(input);
        }

        private void ConsumeWhiteSpace()
        {
            while (Char.IsWhiteSpace(scanner.Char))
            {
                scanner.Next();
            }
        }

        private string ReadIdentifier()
        {
            StringBuilder sb = new StringBuilder();

            while (Char.IsLetterOrDigit(scanner.Char))
            {
                sb.Append(scanner.Char);
                scanner.Next();
            }

            return sb.ToString();
        }

        private string ReadNumber()
        {
            StringBuilder sb = new StringBuilder();

            while (Char.IsDigit(scanner.Char))
            {
                sb.Append(scanner.Char);
                scanner.Next();
            }

            return sb.ToString();
        }

        private void Scan(out Token token)
        {
            token = new Token(TokenType.Unknown);

            ConsumeWhiteSpace();

            if (Char.IsLetter(scanner.Char))
            {
                token = new Token(TokenType.Id, ReadIdentifier());
            }
            else if (Char.IsDigit(scanner.Char))
            {
                token = new Token(TokenType.Num, ReadNumber());
            }
            else
            {
                if (scanner.Char == '\0')
                {
                    token = new Token(TokenType.Eof);
                }
                else if (scanner.Char == '{')
                {
                    token = new Token(TokenType.LBrace);
                }
                else if (scanner.Char == '}')
                {
                    token = new Token(TokenType.RBrace);
                }
                else if (scanner.Char == '(')
                {
                    token = new Token(TokenType.LParen);
                }
                else if (scanner.Char == ')')
                {
                    token = new Token(TokenType.RParen);
                }
                else if (scanner.Char == ',')
                {
                    token = new Token(TokenType.Comma);
                }
                else
                {
                    Console.WriteLine("Unknown token {0} at position {1}", scanner.Char, scanner.Position);
                }
                scanner.Next();
            }
        }

        public void Tokenize()
        {
            Token token;
            Scan(out token);
            Console.WriteLine(token);

            while (token.Type != TokenType.Eof)
            {
                Scan(out token);
                Console.WriteLine(token);
            }
        }
    }
}
