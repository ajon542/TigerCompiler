
namespace TigerCompiler
{
    using System;
    using System.Text;

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

            if (Ch == '\n')
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
}
