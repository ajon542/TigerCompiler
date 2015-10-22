
namespace TigerCompiler
{
    using System;
    using System.Text;

    enum Token
    {
        Unknown,
        Id,
        Num
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
            /*else if (Char.IsDigit(input[position]))
            {
                string number = ReadNumber(input, ref position);
                Console.WriteLine("NUM({0})", number);
                token = Token.Num;
            }*/

            return token;
        }

        public void Tokenize()
        {
            Token token = Scan();
        }
    }
}
