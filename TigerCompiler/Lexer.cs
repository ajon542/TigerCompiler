
namespace TigerCompiler
{
    using System;
    using System.Text;

    enum Token
    {
        Unknown,
        Id,
        Num, 
        Eof
    }

    class Scanner
    {
        private readonly string input;
        private int position;

        public char Char { get; private set; }

        public Scanner(string input)
        {
            this.input = input;
            Char = ' '; // Default to whitespace, it will be consumed on a scan.
        }

        public void Next()
        {
            if (position < input.Length)
            {
                Char = input[position++];
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

        private Token Scan()
        {
            Token token = Token.Unknown;

            ConsumeWhiteSpace();

            if (Char.IsLetter(scanner.Char))
            {
                string identifier = ReadIdentifier();
                Console.WriteLine("ID({0})", identifier);
                token = Token.Id;
            }
            else if (Char.IsDigit(scanner.Char))
            {
                string number = ReadNumber();
                Console.WriteLine("NUM({0})", number);
                token = Token.Num;
            }
            else
            {
                if (scanner.Char == '\0')
                {
                    token = Token.Eof;
                }
            }

            return token;
        }

        public void Tokenize()
        {
            Token token = Scan();

            while (token != Token.Unknown && token != Token.Eof)
            {
                token = Scan();
            }
        }
    }
}
