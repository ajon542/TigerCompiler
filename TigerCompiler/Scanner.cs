
namespace TigerCompiler
{
    using System;
    using System.Text;

    /// <summary>
    /// A scanner takes iterates over an input string and provides
    /// the next character to the caller when requested.
    /// </summary>
    class Scanner
    {
        /// <summary>
        /// The current position of the scanner in regards to the input string.
        /// </summary>
        private int inputIndex;

        /// <summary>
        /// The input string.
        /// </summary>
        private readonly string input;

        /// <summary>
        /// Gets the current line of the input string.
        /// </summary>
        /// <remarks>
        /// A new line occurs after encountering a '\n' character.
        /// </remarks>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the current offset into the line.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets the current character indexed by the scanner.
        /// </summary>
        public char Ch { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Scanner"/> class.
        /// </summary>
        /// <param name="input">The input string to scan.</param>
        public Scanner(string input)
        {
            this.input = input;
            Ch = ' '; // Default to whitespace, it will be consumed on a scan.
        }

        /// <summary>
        /// Update the scanner to point to the next character.
        /// </summary>
        public void Next()
        {
            if (inputIndex < input.Length)
            {
                Ch = input[inputIndex++];
                Column++;
            }
            else
            {
                Ch = '\0';
            }

            if (Ch == '\n')
            {
                Column = 0;
                Line++;
            }
        }

        /// <summary>
        /// Update the scanner to point to the previous character.
        /// </summary>
        public void Prev()
        {
            // TODO: Need to take care updating the scanner state if reversing over a newline.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Consume any whitespace in the input.
        /// </summary>
        public void ConsumeWhiteSpace()
        {
            while (Char.IsWhiteSpace(Ch))
            {
                Next();
            }
        }

        /// <summary>
        /// Read an string or identifier from the input.
        /// </summary>
        /// <returns>A string.</returns>
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

        /// <summary>
        /// Read a number from the input.
        /// </summary>
        /// <returns>A number.</returns>
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
