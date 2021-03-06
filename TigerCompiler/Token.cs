﻿
namespace TigerCompiler
{
    using System.Text;

    enum TokenType
    {
        Unknown,
        Eof,
        Id,
        Num,
        LBrace,
        RBrace,
        LParen,
        RParen,
        Comma,
        Semicolon,

        Plus,
        Minus,
        Mul,
        Div,


        // Keywords
        If,
        Else,

        // Test Tokens
        A,
        B
    }

    /// <summary>
    /// A token is an abstract symbol representing a kind of lexical unit.
    /// </summary>
    class Token
    {
        /// <summary>
        /// Gets the type of token.
        /// </summary>
        public TokenType Type { get; private set; }

        /// <summary>
        /// Gets the value for the token. May be the empty string if the token has no value..
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the line number of the token.
        /// </summary>
        public int Line { get; private set; }

        /// <summary>
        /// Gets the column number of the token.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="type">The type of token.</param>
        /// <param name="line">The line number of the token.</param>
        /// <param name="column">The column number of the token.</param>
        /// <param name="value">The value for the token.</param>
        public Token(TokenType type, int line, int column, string value = "")
        {
            Type = type;
            Line = line;
            Column = column;
            Value = value;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", Type);
            if (Value != string.Empty)
            {
                sb.AppendFormat("({0})", Value);
            }

            return sb.ToString();
        }
    }
}
